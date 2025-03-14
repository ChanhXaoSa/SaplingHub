﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH_BusinessObjects.Common.Model.Account
{
    public class AccountModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
    }
}
