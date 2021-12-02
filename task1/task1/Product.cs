using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OnlineShop
{
    //CLASS PRODUCT
    //Implements comparable
    public class Product:IComparable<Product>
    {
        //FIELDS
        private int id;
        private string name;
        private double price;
        private DateTime expireDate;
        private DateTime manufDate;

        //PARAMETERS
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public DateTime ExpireDate { get => expireDate; set => expireDate = value; }
        public DateTime ManufDate { get => manufDate; set => manufDate = value; }

        // CONSTRUCTOR (zero Patameters)
        public Product()
        {
            this.id = 000;
            this.name = "noName";
            this.price = 0.0;
            this.expireDate = new DateTime(2000, 01, 01);
            this.manufDate = new DateTime(2000, 01, 01);
        }
        // CONSTRUCTOR (with some Patameters)
        public Product(int theId, string theName, double thePrice)
        {
            this.id = theId;
            this.name = theName;
            this.price = thePrice;
            this.expireDate = new DateTime(2000, 01, 01);
            this.manufDate = new DateTime(2000, 01, 01);
        }
        // CONSTRUCTOR (with all Patameters)
        public Product(int theId, string theName, double thePrice, DateTime theExpireDate, DateTime theManufDate)
        {
            this.id = theId;
            this.name = theName;
            this.price = thePrice;
            this.expireDate = theExpireDate;
            this.manufDate = theManufDate;
        }

        //METHODS
        //To check if the product is expired
        public bool CheckExpiration()
        {
            DateTime curratenDate = DateTime.Now;
            string[] expired = (Convert.ToString(this.expireDate - curratenDate)).Split(':');
            return double.Parse(expired[0]) < 0;
        }
        //To check print the product without "line skip" at the end for "Class Basket" purpose
        public string PrintProductBasket()
        {
            string dateE = this.expireDate.ToString("yyyy-MM-dd");
            string dateM = this.manufDate.ToString("yyyy-MM-dd");
            bool isExpired = CheckExpiration();
            Console.Write(this.id + "\t" + this.name + "\t\t$" + "{0:N2}", this.price + "\t" + dateM + "\t" + dateE + "\t" + isExpired);
            return null;
        }

        //OVERRIDING METHODS
        //To print a product without return
        public override string ToString()
        {
            string dateE = this.expireDate.ToString("yyyy-MM-dd");
            string dateM = this.manufDate.ToString("yyyy-MM-dd");
            bool isExpired = CheckExpiration();
            Console.WriteLine(this.id + "\t" + this.name + "\t\t$" + "{0:N2}", this.price + "\t" + dateM + "\t" + dateE + "\t" + isExpired);
            return null;
        }
        //To compare two products with both names (on lower case) and expired date.
        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   this.name.ToLower() == product.name.ToLower() &&
                   this.expireDate == product.expireDate;
        }
        //CompareTo method defined for sort puposes
        public int CompareTo(Product otherProduct)
        {
            if (this.expireDate < otherProduct.ExpireDate)
                return -1;
            else if (this.expireDate > otherProduct.ExpireDate)
                return 1;
            else
                return 0;
        }

        //Override GetHashCode (ASK)
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

    }
}
