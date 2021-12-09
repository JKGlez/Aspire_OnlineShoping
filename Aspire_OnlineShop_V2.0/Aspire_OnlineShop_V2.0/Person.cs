using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    abstract class Person
    {
        //FIELDS
        private string name;
        private string email;
        private long mobile;
        private string addres;

        //PROPIERTIES
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public long Mobile { get => mobile; set => mobile = value; }
        public string Addres { get => addres; set => addres = value; }

        //CONSTRUCTOR
        protected Person(string name, string email, long mobile, string addres)
        {
            this.name = name;
            this.email = email;
            this.mobile = mobile;
            this.addres = addres;
        }

        public override string ToString()
        {
            return (Name + "\t" + Email + "\t" + Mobile);



        }
    }
}
