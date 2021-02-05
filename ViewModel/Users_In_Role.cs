﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication11.Models;

namespace WebApplication11.ViewModels
{
    public class Users_In_Role
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string WorkingPlace { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}