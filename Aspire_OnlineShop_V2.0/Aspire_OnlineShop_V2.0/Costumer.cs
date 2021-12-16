using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class Costumer : Person
    {
        private double wallet;
        private string password;

        public double Wallet { get => wallet; set => wallet = value; }
        public string Password { get => password; set => password = value; }

        public Costumer(string name, string email, long mobile, string addres, string password, double wallet) : base(name, email, mobile, addres)
        {
            this.Wallet = wallet;
            this.Password = password;
        }


    }
}
