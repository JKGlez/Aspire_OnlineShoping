using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class Electronic : Product, INoPerishable
    {
        //FIELDS
        private DateTime guarantee;

        //PROPIERTIES
        public DateTime Guarantee { get => guarantee; set => guarantee = value; }

        //CONSTRUCTOR
        public Electronic(int id, string category, string name, double price, DateTime guarateeTime) : base(id, category, name, price)
        {
            this.Guarantee = guarateeTime;
        }

        //METHODS FROM "INOPerishable"
        public DateTime GuaranteedUntil()
        {
            return this.Guarantee;
        }

        public void ExtendsGuarantee()
        {
            int nMonths = int.Parse(Console.ReadLine());
            this.Guarantee.AddMonths(nMonths);
            
        }


        //ABSTRACTS METHODS (PRODUCT)
        public override string PrintProduct()
        {
            Console.WriteLine("ID \tCategory \tName \tPrice  \tGuaranteed until");
            string dateG = this.Guarantee.ToString("yyyy-MM-dd");
            return this.ToString() + dateG;
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
