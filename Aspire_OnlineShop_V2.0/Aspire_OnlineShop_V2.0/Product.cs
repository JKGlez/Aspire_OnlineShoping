using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    abstract class Product
    {
        //FIELDS
        private int id;
        private string category;
        private string name;
        private double price;
        private string description;

        //PROPIERTIES
        public int Id { get => id; set => id = value; }
        public string Category { get => category; set => category = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public string Description { get => description; set => description = value; }

        //CONSTRUCTOR
        protected Product(int id, string category, string name, double price)
        {
            this.id = id;
            this.category = category;
            this.name = name;
            this.price = price;
        }


        //OVERRIDED METHODS
        //To print a product without return
        public override string ToString()
        {
            return (Id + "\t" + Category + "\t" + Name + "\t$" + Price + "\t");
            
        }

        //Abstracts methods
        public abstract string PrintProduct();
        public abstract string GetDescription();
        public abstract void SetDescription(string d);


    }
}
