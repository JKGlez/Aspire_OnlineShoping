using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class Grocery: Product, IPerishable
    {
        //FIELDS
        private DateTime manufDate;
        private DateTime expireDate;

        //PROPIERTIES
        public DateTime ManufDate { get => manufDate; set => manufDate = value; }
        public DateTime ExpireDate { get => expireDate; set => expireDate = value; }
        
        //CONSTRUCTOR
        public Grocery(int id,string category, string name, double price, DateTime expireDate, DateTime manufDate) : base(id, category, name, price)
        {
            this.ExpireDate = expireDate;
            this.ManufDate = manufDate;
        }


        //METHODS FROM "IPerishable"
        public bool IsExpired()
        {
            DateTime curratenDate = DateTime.Now;
            string[] expired = Convert.ToString(this.ExpireDate - curratenDate).Split(':');
            return double.Parse(expired[0]) < 0;
        }

        public int TimeToExpire()
        {
            DateTime curratenDate = DateTime.Now;
            TimeSpan tSpan = this.ExpireDate - curratenDate;
            return tSpan.Days;
        }

        //ABSTRACTS METHODS (PRODUCT)
        public override string PrintProduct()
        {
            Console.WriteLine("ID \tCategory \tName \tPrice  \tManufactured \tExpire date \tExpired?");
            string dateE = this.expireDate.ToString("yyyy-MM-dd");
            string dateM = this.manufDate.ToString("yyyy-MM-dd");
            bool isExpired = this.IsExpired();
            return this.ToString() + dateM + "\t" + dateE + "\t" + isExpired;
        }

        public override string GetDescription() 
        { 
            return this.Description; 
        }

        public override void SetDescription(string d) 
        { 
            Description = d; 
        }
    }
}
