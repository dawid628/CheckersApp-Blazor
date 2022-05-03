﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckersApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int Score { get; set; }
    }
}
