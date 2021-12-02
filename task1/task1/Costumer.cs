using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    class Costumer:IComparable<Costumer>
    {
        //FIELDS
        private string name;
        private string email;
        private long mobile;
        private string addres;
        

        // CONSTRUCTOR (zero Patameters)
        public Costumer()
        {
            this.name = "newCostumer";
            this.email = "no@Email.com";
            this.mobile = 1234567890;
            this.addres = "noAddres";
        }
        // CONSTRUCTOR (with all Patameters)
        public Costumer(string name, string email, long mobile, string addres)
        {
            this.name = name;
            this.email = email;
            this.mobile = mobile;
            this.addres = addres;
        }

        //PARAMETERS
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public long Mobile { get => mobile; set => mobile = value; }
        public string Addres { get => addres; set => addres = value; }
        

        public int CompareTo(Costumer otherCostumer)
        {
            char[] thisRevertedEmail = this.email.ToCharArray();
            Array.Reverse(thisRevertedEmail);
            char[] otherRevertedEmail = otherCostumer.Email.ToCharArray();
            Array.Reverse(otherRevertedEmail);
            string tRE = new string(thisRevertedEmail);
            string oRE = new string(otherRevertedEmail);
            return (String.Compare(tRE, oRE, comparisonType: StringComparison.OrdinalIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            return obj is Costumer costumer &&
                   email == costumer.email &&
                   mobile == costumer.mobile;
        }
        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }
        public override string ToString()
        {
            string s = ("Costumer: " + this.name + ", " + this.email + ", " + this.mobile + ", " + this.addres);
            return s;
        }
    }
}
