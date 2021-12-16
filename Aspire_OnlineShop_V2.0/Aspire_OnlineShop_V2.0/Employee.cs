﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class Employee : Person
    {
        private bool adminRights;
        private string password;

        public bool AdminRights { get => adminRights; set => adminRights = value; }
        public string Password { get => password; set => password = value; }

        public Employee(string name, string email, long mobile, string addres, string password, bool adminRights) : base(name, email, mobile, addres)
        {
            this.AdminRights = adminRights;
            this.Password = password;
        }

        public string GetEmployeeString()
        {
            return Name + "," + Email + "," + Mobile + "," + Addres + "," + Password + "," + AdminRights;
        }

    }
}
