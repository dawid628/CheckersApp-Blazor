#pragma checksum "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a32950322f64629e04bd61f9964311feb44bfcf4"
// <auto-generated/>
#pragma warning disable 1591
namespace CheckersApp.Client.Pages
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
#line 2 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenComponent<Microsoft.AspNetCore.Components.Authorization.AuthorizeView>(0);
            __builder.AddAttribute(1, "Authorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
#nullable restore
#line 54 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
         if (inGame)
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenComponent<CheckersApp.Client.Checkerboard>(2);
                __builder2.AddAttribute(3, "HubConnection", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.SignalR.Client.HubConnection>(
#nullable restore
#line 56 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
                                         hubConnection

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(4, "TableId", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.String>(
#nullable restore
#line 56 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
                                                                  tableId

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(5, "IsWhitePlayer", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 56 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
                                                                                          isWhite

#line default
#line hidden
#nullable disable
                ));
                __builder2.CloseComponent();
#nullable restore
#line 57 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
        }
        else
        {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(6, "button");
                __builder2.AddAttribute(7, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 60 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
                              CreateGame

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddContent(8, "Create Game");
                __builder2.CloseElement();
#nullable restore
#line 61 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
             foreach (string table in tables)
            {

#line default
#line hidden
#nullable disable
                __builder2.OpenElement(9, "button");
                __builder2.AddAttribute(10, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 63 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
                                  () => JoinGame(table)

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddContent(11, "Join Game ");
                __builder2.AddContent(12, 
#nullable restore
#line 63 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
                                                                    tableNames.Where(n => tableId == table)

#line default
#line hidden
#nullable disable
                );
                __builder2.CloseElement();
#nullable restore
#line 64 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 64 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
             
        }

#line default
#line hidden
#nullable disable
            }
            ));
            __builder.AddAttribute(13, "NotAuthorized", (Microsoft.AspNetCore.Components.RenderFragment<Microsoft.AspNetCore.Components.Authorization.AuthenticationState>)((context) => (__builder2) => {
                __builder2.AddMarkupContent(14, "<p><a href=\"/Identity/Account/Login\">Sign in</a> to see this page.</p>");
            }
            ));
            __builder.CloseComponent();
        }
        #pragma warning restore 1998
#nullable restore
#line 5 "C:\Users\User\source\repos\CheckersApp\CheckersApp\Client\Pages\Index.razor"
      
    [CascadingParameter]
    private Task<AuthenticationState> _authState { get; set; }
    private AuthenticationState authState;

    HubConnection hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:44303/connect")
    .Build();

    protected override async Task OnInitializedAsync()
    {
        var authState = await _authState;
        await RefreshTables();
    }

    bool inGame = false;

    async Task RefreshTables()
    {
        HttpClient client = new HttpClient();
        tables = await client.GetFromJsonAsync<List<string>>("https://localhost:44303/api/GetTables");
    }

    async Task CreateGame()
    {
        await hubConnection.StartAsync();
        tableId = Guid.NewGuid().ToString();
        userName = authState.User.Identity.Name;
        tableNames.Add(new KeyValuePair<string,string>(userName, tableId));
        await hubConnection.SendAsync("JoinTable", tableId, userName);
        inGame = true;
    }

    async Task JoinGame(string tableId)
    {
        await hubConnection.StartAsync();
        this.tableId = tableId;
        isWhite = false;
        await hubConnection.SendAsync("JoinTable", tableId);
        inGame = true;
    }
    string tableId = "";
    string userName = "";
    List<string> tables = new List<string>();
    List<KeyValuePair<string, string>> tableNames = new List<KeyValuePair<string, string>>();
    bool isWhite = true;

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
