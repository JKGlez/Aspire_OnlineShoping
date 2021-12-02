using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    class Basket
    {
        //Fields
        private static List<Product> productOnBasket = new List<Product>();
        private static List<int> quantityOnBasket = new List<int>();
        private static double amoutToPay = 0;

        //Propiertes
        public static List<Product> ProductOnBasket { get => productOnBasket; set => productOnBasket = value; }
        public static List<int> QuantityOnBasket { get => quantityOnBasket; set => quantityOnBasket = value; }
        public static double AmoutToPay { get => amoutToPay; set => amoutToPay = value; }


        //Methods
        public void AddProductToBasket(Product productToAdd, int quantity)
        {
            productOnBasket.Add(productToAdd);
            quantityOnBasket.Add(quantity);
            amoutToPay += productToAdd.Price * quantity;
            Console.WriteLine("PRODUCT ADDED TO BASKET");
            Console.WriteLine("Current total to pay: "+amoutToPay);
        }

        public void PrintBasket()
        {
            Console.WriteLine("ID \tNAME \t\tPRICE  \tManufactured \tExpire date \tExpired? \tQuantity \tTotal");
            for (int i = 0; i < productOnBasket.Count; i++)
            {
                Console.WriteLine(productOnBasket[i].PrintProductBasket() + "\t\t" + quantityOnBasket[i] + "\t\t$" + (quantityOnBasket[i] * productOnBasket[i].Price));
            }
            Console.WriteLine("CURRENT TOTAL FOR THE BASKET: $" + amoutToPay);
                
        }
    }
}
