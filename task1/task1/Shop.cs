using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Shop
    {

        //Fields
        private Product[] shopsProductsPD = new Product[5];
        private List<Product> shopsProductsOL = new List<Product>();

        private Costumer[] shopsCostumersPD = new Costumer[4];
        private List<Costumer> shopsCostumersOL = new List<Costumer>();

        //Propierties
        public Product[] ShopsProductsPD { get => shopsProductsPD; set => shopsProductsPD = value; }
        public List<Product> ShopsProductsOL { get => shopsProductsOL; set => shopsProductsOL = value; }
        internal List<Costumer> ShopsCostumersOL { get => shopsCostumersOL; set => shopsCostumersOL = value; }
        internal Costumer[] ShopsCostumersPD { get => shopsCostumersPD; set => shopsCostumersPD = value; }


        //Methods
        public void RegisterProductPD()
        {
            int[] ids = { 123, 234, 345, 567, 678 };
            string[] names = { "Milk", "Eggs", "Flour", "Ham", "Bread" };
            double[] prices = { 25.00, 5.00, 20.00, 37.50, 45.50 };
            DateTime[] expireDates = { new DateTime(2021, 12, 10), new DateTime(2021, 11, 21), new DateTime(2022, 02, 01), new DateTime(2021, 11, 25), new DateTime(2021, 12, 15) };
            DateTime[] manufDates = { new DateTime(2021, 11, 01), new DateTime(2021, 11, 03), new DateTime(2021, 08, 25), new DateTime(2021, 11, 08), new DateTime(2021, 11, 05) };
            for (int i = 0; i < ShopsProductsPD.Length; i++)
            {
                ShopsProductsPD[i] = new Product(ids[i], names[i], prices[i], expireDates[i], manufDates[i]);
            }
            Console.WriteLine("Predefined products registered successfully! (5/5)");
        }
        public  void RegisterProductM()
        {
            string product;
            string[] productData;
            string productName;
            DateTime productManufacturingDate;
            DateTime productExpiringDate;

            Console.WriteLine("Enter the datails of the product on one line:");
            Console.WriteLine("(ID,ProductName,Price,ExpireDate,ManufDate)");
            Console.WriteLine("Example:");
            Console.WriteLine("(123,Eggs,5,2021-11-30,2021-11-20)");

            product = Console.ReadLine();
            productData = product.Split(',');

            int.TryParse(productData[0], out int productId);
            productName = productData[1];
            double.TryParse(productData[2], out double productPrice);
            string[] YMD = productData[3].Split('-');
            productExpiringDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
            YMD = productData[4].Split('-');
            productManufacturingDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));

            Product currentProduct = new Product(productId, productName, productPrice, productExpiringDate, productManufacturingDate);
            ShopsProductsOL.Add(currentProduct);
            Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
        }
        public  void RegisterCostumerPD()
        {
            shopsCostumersPD[0] = (new Costumer("Juan", "Juan@email.com", 1234567890, "street 1 numer 1"));
            shopsCostumersPD[1] = (new Costumer("Jose", "Jose@email.com", 2345678901, "street 2 numer 2"));
            shopsCostumersPD[2] = (new Costumer("David", "David@email.com", 3456789012, "street 3 numer 3"));
            shopsCostumersPD[3] = (new Costumer("Erick", "Erick@email.com", 4567890123, "street 4 numer 4"));
            Console.WriteLine("Predefined Costumers registered successfully! (4/4)");
        }
        public void RegisterCostumerM()
        {
            string costumer;
            string[] costumerData;
            string name;
            string email;
            string addres;

            Console.WriteLine("Enter the datails of the costumer on one line:");
            Console.WriteLine("(Name,Email,Mobile,Address)");
            Console.WriteLine("Example:");
            Console.WriteLine("(Juan,Juan@email.com,1234567890,Street 1 number 1)");

            costumer = Console.ReadLine();
            costumerData = costumer.Split(',');

            name = costumerData[0];
            email = costumerData[1];
            long.TryParse(costumerData[2], out long mobile);
            addres = costumerData[3];

            Costumer currentCostumer = new Costumer(name, email, mobile, addres);
            ShopsCostumersOL.Add(currentCostumer);
            Console.WriteLine("COSTUMER ADDED SUCCEFULLY");
        }
        public  void SearchPriceByName(string nameToSearch)
        {
            string currentName;
            for (int i = 0; i < ShopsProductsPD.Length; i++)
            {
                currentName = ShopsProductsPD[i].Name.ToLower();
                if (currentName.CompareTo(nameToSearch.ToLower()) == 0)
                {
                    Console.WriteLine("Product FOUND!");
                    Console.WriteLine("The PRICE of the " + nameToSearch + " is: $" + ShopsProductsPD[i].Price);
                    return;
                }
            }
            Console.WriteLine("There is not " + nameToSearch + " register as a product.");
        }
        public  void SearchProductByManfDate(DateTime manfDateToSearch)
        {
            DateTime manfDateCurrentProduct;
            for (int i = 0; i < ShopsProductsPD.Length; i++)
            {
                manfDateCurrentProduct = ShopsProductsPD[i].ManufDate;
                if (DateTime.Compare(manfDateCurrentProduct, manfDateToSearch) == 0)
                {
                    Console.WriteLine("Product FOUND!");
                    Console.WriteLine(ShopsProductsPD[i]);
                    return;
                }
            }
            Console.WriteLine("There is not product register with " + manfDateToSearch.ToString("yyyy-MM-dd") + " as a mufacturing date.");
        }
        public  bool PrintRegisteredProducts()
        {
            Console.WriteLine("ID \tNAME \t\tPRICE  \tManufactured \tExpire date \tExpired?");
            if (ShopsProductsOL.Count == 0)
            {
                for (int i = 0; i < ShopsProductsPD.Length; i++)
                {
                    Console.WriteLine(ShopsProductsPD[i]);
                }
                return true;
            }
            else
            {
                foreach (Product product in ShopsProductsOL)
                {
                    Console.WriteLine(product);
                }
                return false;
            }
        }
        public  bool PrintRegisteredCostumers()
        {
            if (ShopsCostumersOL.Count == 0)
            {
                for (int i = 0; i < ShopsCostumersPD.Length; i++)
                {
                    Console.WriteLine(ShopsCostumersPD[i]);
                }
                return true;
            }
            else
            {
                foreach (Costumer costumer in ShopsCostumersOL)
                {
                    Console.WriteLine(costumer);
                }
                return false;
            }
        }
        public  int GetIndexByMenu(bool pPD, int pID)
        {
            if (pPD)
            {
                for (int i = 0; i < ShopsProductsPD.Length; i++)
                {
                    if (ShopsProductsPD[i].Id == pID)
                        return i;
                }
            }
            else
            {
                for (int i = 0; i < ShopsProductsOL.Count; i++)
                {
                    if (ShopsProductsPD[i].Id == pID)
                        return i;
                }
            }
            return -1;
        }
        public  void SortByExpiringdate(bool pPD)
        {
            if (pPD)
            {
                for (int i = 0; i < ShopsProductsPD.Length; i++)
                {
                    Product curretnProduct = ShopsProductsPD[i];
                    if (i < ShopsProductsPD.Length - 1)
                    {
                        Product nextProduct = ShopsProductsPD[i+1];
                        if (curretnProduct.CompareTo(nextProduct) > 0)
                        {
                            ShopsProductsPD[i] = nextProduct;
                            ShopsProductsPD[i+1] = curretnProduct;
                            i = 0;
                        }
                    }

                }
            }
            else
            {
                ShopsProductsOL.Sort();
            }
        }
        public  void SortCostumers()
        {
            if (shopsCostumersOL.Count == 0)
            {
                for (int i = 0; i < shopsCostumersPD.Length; i++)
                {
                    Costumer currentCostumer = shopsCostumersPD[i];
                    if (i < shopsCostumersPD.Length - 1)
                    {
                        Costumer nextCostumer = shopsCostumersPD[i + 1]; 
                        if (currentCostumer.CompareTo(nextCostumer) > 0) 
                        {
                            ShopsCostumersPD[i] = nextCostumer;
                            ShopsCostumersPD[i+1] = currentCostumer;
                            i = 0;
                        }
                    }

                }
            }
            else
            {
                ShopsCostumersOL.Sort();
            }
            Console.WriteLine("-> List of costumer now in order: ");
            PrintRegisteredCostumers();

        }

    }
   }
