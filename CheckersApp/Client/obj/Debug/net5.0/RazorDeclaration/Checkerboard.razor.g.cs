// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace CheckersApp.Client
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using CheckersApp.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\_Imports.razor"
using CheckersApp.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Checkerboard.razor"
using Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Checkerboard.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
    public partial class Checkerboard : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 4 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Checkerboard.razor"
       
    [Parameter] public HubConnection HubConnection { get; set; }
    [Parameter] public string TableId { get; set; }
    [Parameter] public string playerName { get; set; }
    [Parameter] public bool IsWhitePlayer { get; set; }

    List<Checker> blackCheckers = new List<Checker>();
    List<Checker> whiteCheckers = new List<Checker>();
    List<Message> messages = new List<Message>();

    protected string Context { get; set; } = "";
    string whitePlayer = "";
    string blackPlayer = "";
    bool secondPlayerJoined = false;

    protected override void OnInitialized()
    {

        if (IsWhitePlayer)
        {
            whitePlayer = playerName;
        }

        if (!IsWhitePlayer)
        {
            blackPlayer = playerName;
            secondPlayerJoined = true;
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = (i + 1) % 2; j < 8; j += 2)
            {
                blackCheckers.Add(new Checker
                {
                    Color = "black",
                    Column = j,
                    Row = i,
                    Direction = CheckerDirection.Down
                });
            }
        }

        for (int i = 5; i < 8; i++)
        {
            for (int j = (i + 1) % 2; j < 8; j += 2)
            {
                whiteCheckers.Add(new Checker
                {
                    Color = "white",
                    Column = j,
                    Row = i,
                    Direction = CheckerDirection.Up
                });
            }
        }

        HubConnection.On<int, int, int, int>("Move", ServerMove);
        HubConnection.On<string, string>("Message", (playername, context) => {
            messages.Add(new Message(playername, context));
            refreshMessages();
        });
        HubConnection.On("TableJoined", () =>
        {
            secondPlayerJoined = true;
            StateHasChanged();
        });
        HubConnection.On("SamePlayer", () => {
            winner = "Nie możesz zagrać ze sobą!";
            gameOn = false;
            StateHasChanged();
        });

        void ServerMove(int previousColumn, int previousRow, int newColumn, int newRow)
        {
            var checker = blackCheckers.FirstOrDefault(n => n.Column == previousColumn && n.Row == previousRow);
            if (checker == null)
            {
                checker = whiteCheckers.FirstOrDefault(n => n.Column == previousColumn && n.Row == previousRow);
            }
            activeChecker = checker;
            EvaluateCheckerSpots();
            MoveChecker(newRow, newColumn);
        }

    }

    Checker activeChecker = null;
    List<(int row, int column)> cellsPossible = new();
    string winner = "";
    int emptyMoves = 0;


    public void refreshMessages()
    {
        StateHasChanged();
    }

    void EvaluateGameStatus()
    {
        if (blackCheckers.Count == 0)
        {
            if (IsWhitePlayer)
            {
                winner = "Wygrana";
                HubConnection.SendAsync("UpdateScore", playerName, true);
            }
            if (!IsWhitePlayer)
            {
                winner = "Porażka";
                HubConnection.SendAsync("UpdateScore", playerName, false);
            }
            gameOn = false;
        }
        if (whiteCheckers.Count == 0)
        {
            if (!IsWhitePlayer)
            {
                winner = "Wygrana";
                HubConnection.SendAsync("UpdateScore", playerName, true);
            }
            if (IsWhitePlayer)
            {
                winner = "Porażka";
                HubConnection.SendAsync("UpdateScore", playerName, false);
            }
            gameOn = false;
        }
        if (emptyMoves == 20)
        {
            gameOn = false;
            winner = "Remis";
        }
    }

    void EvaluateCheckerSpots()
    {
        cellsPossible.Clear();
        if (activeChecker != null)
        {
            List<int> rowsPossible = new List<int>();
            if (activeChecker.Direction == CheckerDirection.Down ||
                activeChecker.Direction == CheckerDirection.Both)
            {
                rowsPossible.Add(activeChecker.Row + 1);
            }
            if (activeChecker.Direction == CheckerDirection.Up ||
                activeChecker.Direction == CheckerDirection.Both)
            {
                rowsPossible.Add(activeChecker.Row - 1);
            }

            foreach (var row in rowsPossible)
            {
                EvaluateSpot(row, activeChecker.Column - 1);
                EvaluateSpot(row, activeChecker.Column + 1);
            }
        }
    }
    bool hasJumps(Checker checker)
    {
        Checker enemyChecker;
        Checker nextChecker;

        //has white jumps
        if (checker.Direction == CheckerDirection.Up)
        {
            enemyChecker = blackCheckers.FirstOrDefault(
            n => n.Column == checker.Column + 1 && n.Row == checker.Row - 1);

            if (enemyChecker != null)
            {
                nextChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row - 1);
                if (nextChecker == null && enemyChecker.Column < 7 && enemyChecker.Row > 1)
                {
                    nextChecker = whiteCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row - 1);
                    if (nextChecker == null && enemyChecker.Column < 7 && enemyChecker.Row > 1)
                    {
                        temp = "bicia są obowiązkowe!";
                        return true;
                    }
                }

            }

            enemyChecker = blackCheckers.FirstOrDefault(
                n => n.Column == checker.Column - 1 && n.Row == checker.Row - 1);
            if (enemyChecker != null)
            {
                nextChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row - 1);
                if (nextChecker == null && enemyChecker.Column > 1 && enemyChecker.Row > 1)
                {
                    nextChecker = whiteCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row - 1);
                    if (nextChecker == null && enemyChecker.Column > 1 && enemyChecker.Row > 1)
                    {
                        temp = "bicia są obowiązkowe!";
                        return true;
                    }
                }

            }
        }
        //has black jumps
        if (checker.Direction == CheckerDirection.Down)
        {
            enemyChecker = whiteCheckers.FirstOrDefault(
            n => n.Column == checker.Column + 1 && n.Row == checker.Row + 1);

            if (enemyChecker != null)
            {
                nextChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row + 1);
                if (nextChecker == null && enemyChecker.Column < 7 && enemyChecker.Row < 7)
                {
                    nextChecker = whiteCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row + 1);
                    if (nextChecker == null && enemyChecker.Column < 7 && enemyChecker.Row < 7)
                    {
                        temp = "bicia są obowiązkowe!";
                        return true;
                    }
                }

            }

            enemyChecker = whiteCheckers.FirstOrDefault(
                n => n.Column == checker.Column - 1 && n.Row == checker.Row + 1);
            if (enemyChecker != null)
            {
                nextChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row + 1);
                if (nextChecker == null && enemyChecker.Column > 1 && enemyChecker.Row < 7)
                {
                    nextChecker = whiteCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row + 1);
                    if (nextChecker == null && enemyChecker.Column > 1 && enemyChecker.Row < 7)
                    {
                        temp = "bicia są obowiązkowe!";
                        return true;
                    }
                }
            }
        }
        // can queen jump?
        if (checker.Direction == CheckerDirection.Both)
        {
            if (checker.Color == "white")
            {
                enemyChecker = blackCheckers.FirstOrDefault(
                n => n.Column == checker.Column + 1 && n.Row == checker.Row - 1);

                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row - 1);
                    if (nextChecker == null && enemyChecker.Column < 7)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row - 1);
                        if (nextChecker == null && enemyChecker.Column < 7)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }

                }

                enemyChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == checker.Column - 1 && n.Row == checker.Row - 1);
                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row - 1);
                    if (nextChecker == null && enemyChecker.Column > 1)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row - 1);
                        if (nextChecker == null && enemyChecker.Column > 1)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }

                }

                // next row
                enemyChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == checker.Column + 1 && n.Row == checker.Row + 1);

                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row + 1);
                    if (nextChecker == null && enemyChecker.Column < 7)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row + 1);
                        if (nextChecker == null && enemyChecker.Column < 7)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }

                }

                enemyChecker = blackCheckers.FirstOrDefault(
                    n => n.Column == checker.Column - 1 && n.Row == checker.Row + 1);
                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row + 1);
                    if (nextChecker == null && enemyChecker.Column > 1)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row + 1);
                        if (nextChecker == null && enemyChecker.Column > 1)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }

                }
            }

            //
            if (checker.Color == "black")
            {
                enemyChecker = whiteCheckers.FirstOrDefault(
                n => n.Column == checker.Column + 1 && n.Row == checker.Row + 1);

                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row + 1);
                    if (nextChecker == null && enemyChecker.Column < 7)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row + 1);
                        if (nextChecker == null && enemyChecker.Column < 7)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }

                }

                enemyChecker = whiteCheckers.FirstOrDefault(
                    n => n.Column == checker.Column - 1 && n.Row == checker.Row + 1);
                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row + 1);
                    if (nextChecker == null && enemyChecker.Column > 1)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row + 1);
                        if (nextChecker == null && enemyChecker.Column > 1)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }
                }

                // next row
                enemyChecker = whiteCheckers.FirstOrDefault(
                    n => n.Column == checker.Column + 1 && n.Row == checker.Row - 1);

                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row - 1);
                    if (nextChecker == null && enemyChecker.Column < 7)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column + 1 && n.Row == enemyChecker.Row - 1);
                        if (nextChecker == null && enemyChecker.Column < 7)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }

                }

                enemyChecker = whiteCheckers.FirstOrDefault(
                    n => n.Column == checker.Column - 1 && n.Row == checker.Row - 1);
                if (enemyChecker != null)
                {
                    nextChecker = blackCheckers.FirstOrDefault(
                        n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row - 1);
                    if (nextChecker == null && enemyChecker.Column > 1)
                    {
                        nextChecker = whiteCheckers.FirstOrDefault(
                            n => n.Column == enemyChecker.Column - 1 && n.Row == enemyChecker.Row - 1);
                        if (nextChecker == null && enemyChecker.Column > 1)
                        {
                            temp = "bicia są obowiązkowe!";
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    void EvaluateSpot(int row, int column, bool firstTime = true)
    {
        var blackChecker = blackCheckers.FirstOrDefault(n => n.Column == column && n.Row == row);
        var whiteChecker = whiteCheckers.FirstOrDefault(n => n.Column == column && n.Row == row);

        if (!canJump)
        {
            if (blackChecker == null && whiteChecker == null)
            {
                cellsPossible.Add((row, column));
            }
            else if (firstTime)
            {
                // can i jump this checker?
                if ((whiteTurn && blackChecker != null) ||
                    (!whiteTurn && whiteChecker != null))
                {
                    int columnDifference = column - activeChecker.Column;
                    int rowDifference = row - activeChecker.Row;

                    EvaluateSpot(row + rowDifference, column + columnDifference, false);
                }
            }
        }
        if (canJump)
        {
            Checker nextChecker;
            int columnDifference = column - activeChecker.Column;
            int rowDifference = row - activeChecker.Row;
            //cellsPossible.Add((row + rowDifference, column + columnDifference));
            if((whiteTurn && blackChecker != null) ||
                    (!whiteTurn && whiteChecker != null))
            {
                nextChecker = blackCheckers.FirstOrDefault(n => n.Column == column + columnDifference && n.Row == row + rowDifference);
                if(nextChecker == null)
                {
                    nextChecker = whiteCheckers.FirstOrDefault(n => n.Column == column + columnDifference && n.Row == row + rowDifference);
                    if(nextChecker == null)
                    {
                        cellsPossible.Add((row + rowDifference, column + columnDifference));
                    }
                }
            }

        }
    }

    void MoveChecker(int row, int column)
    {
        canJump = false;
        bool jumped = false;
        bool canMoveHere = cellsPossible.Contains((row, column));
        if (!canMoveHere)
        {
            return;
        }
        else
        {
            emptyMoves++;
        }

        if (Math.Abs(activeChecker.Column - column) == 2)
        {
            jumped = true;
            // we jumped something
            int jumpedColumn = (activeChecker.Column + column) / 2;
            int jumpedRow = (activeChecker.Row + row) / 2;

            var blackChecker = blackCheckers.FirstOrDefault(
                n => n.Row == jumpedRow && n.Column == jumpedColumn);

            if (blackChecker != null)
            {
                blackCheckers.Remove(blackChecker);
            }


            var whiteChecker = whiteCheckers.FirstOrDefault(
                n => n.Row == jumpedRow && n.Column == jumpedColumn);

            if (whiteChecker != null)
            {
                whiteCheckers.Remove(whiteChecker);
            }

            emptyMoves = 0;
        }
        HubConnection.SendAsync("Move", TableId, activeChecker.Column, activeChecker.Row, column, row);

        activeChecker.Column = column;
        activeChecker.Row = row;

        if (activeChecker.Row == 0 && activeChecker.Color == "white")
        {
            activeChecker.Direction = CheckerDirection.Both;
        }
        if (activeChecker.Row == 7 && activeChecker.Color == "black")
        {
            activeChecker.Direction = CheckerDirection.Both;
        }
        if (jumped)
        {
            if (hasJumps(activeChecker))
            {
                canJump = true;
                EvaluateGameStatus();
                EvaluateCheckerSpots();
                StateHasChanged();
            }
            if (!hasJumps(activeChecker))
            {
                activeChecker = null;
                whiteTurn = !whiteTurn;
                EvaluateGameStatus();
                EvaluateCheckerSpots();
                StateHasChanged();
            }
        }
        if (!jumped)
        {
            activeChecker = null;
            whiteTurn = !whiteTurn;
            EvaluateGameStatus();
            EvaluateCheckerSpots();
            StateHasChanged();
        }

    }

    void CheckerClicked(Checker checker)
    {
        temp = "";
        canJump = false;
        if (whiteTurn != IsWhitePlayer)
        {
            return;
        }
        if (whiteTurn && checker.Color == "black")
            return;
        if (!whiteTurn && checker.Color == "white")
            return;

        if(checker.Color == "white")
        {
            foreach(var check in whiteCheckers)
            {
                if(hasJumps(check))
                {
                    canJump = true;
                    break;
                }
            }
        }

        if (checker.Color == "black")
        {
            foreach (var check in blackCheckers)
            {
                if (hasJumps(check))
                {
                    canJump = true;
                    break;
                }
            }
        }
        if (canJump)
        {
            if (hasJumps(checker))
            {
                activeChecker = checker;
                EvaluateCheckerSpots();
            }
        }
        if(!canJump)
        {
            activeChecker = checker;
            EvaluateCheckerSpots();
        }
    }

    bool whiteTurn = true;
    bool gameOn = true;
    bool canJump = false;
    string temp = "";

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
