//Juan Gonzalez's CODE - task 1 (class) - 26/11/21
using System;
using System.Collections.Generic;

namespace Shop{

    public class Shop {

        //Fields
        private static Product[] shopsProductsPD = new Product[5];
        private static List<Product> shopsProductsOL = new List<Product>();
        private static List<Costumer> shopsCostumers= new List<Costumer>();

        //Propierties
        public static Product[] ShopsProductsPD { get => shopsProductsPD; set => shopsProductsPD = value; }
        public static List<Product> ShopsProductsOL { get => shopsProductsOL; set => shopsProductsOL = value; }
        internal static List<Costumer> ShopsCostumers { get => shopsCostumers; set => shopsCostumers = value; }


        //Methods
        public static void registerProductPD()
        {
            int[] ids = { 123, 234, 345, 567, 678 };
            string[] names = { "Milk", "Eggs", "Flour", "Ham", "Bread" };
            double[] prices = { 25.00, 5.00, 20.00, 37.50, 45.50 };
            DateTime[] expireDates = { new DateTime(2021, 12, 10), new DateTime(2021, 11, 21), new DateTime(2022, 02, 01), new DateTime(2021, 11, 25), new DateTime(2021, 12, 15) };
            DateTime[] manufDates =  { new DateTime(2021, 11, 01), new DateTime(2021, 11, 03), new DateTime(2021, 08, 25), new DateTime(2021, 11, 08), new DateTime(2021, 11, 05) };
            for(int i = 0; i<shopsProductsPD.Length; i++)
            {
                shopsProductsPD[i] = new Product(ids[i],names[i],prices[i],expireDates[i],manufDates[i]);
            }
            Console.WriteLine("Predefined products registered successfully! (5/5)");
        }
        public static void registerProductM()
        {
            string product;
            string[] productData;
            int productId;
            string productName;
            double productPrice;
            DateTime productManufacturingDate;
            DateTime productExpiringDate;

            product = Console.ReadLine();
            productData = product.Split(',');

            int.TryParse(productData[0], out productId);
            productName = productData[1];
            double.TryParse(productData[2], out productPrice);
            string[] YMD = productData[3].Split('-');
            productExpiringDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
            YMD = productData[4].Split('-');
            productManufacturingDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));

            Product currentProduct = new Product(productId, productName, productPrice, productExpiringDate, productManufacturingDate);
            shopsProductsOL.Add(currentProduct);
            Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
        }
        public static void registerCostumerPD()
        {
            shopsCostumers.Add(new Costumer("Juan", "Juan@email.com", 1234567890, "street 1 numer 1"));
            shopsCostumers.Add(new Costumer("Jose", "Jose@email.com", 2345678901, "street 2 numer 2"));
            shopsCostumers.Add(new Costumer("David", "David@email.com", 3456789012, "street 3 numer 3"));
            shopsCostumers.Add(new Costumer("Erick", "Erick@email.com", 4567890123, "street 4 numer 4"));

            Console.WriteLine("Predefined Costumers registered successfully! (4/4)");
        }
        public static void searchPriceByName(string nameToSearch)
        {
            string currentName;
            for(int i = 0;i<shopsProductsPD.Length;i++)
            {
                currentName = shopsProductsPD[i].Name.ToLower();
                if (currentName.CompareTo(nameToSearch.ToLower()) == 0)
                {
                    Console.WriteLine("Product FOUND!");
                    Console.WriteLine("The PRICE of the " + nameToSearch + " is: $" + shopsProductsPD[i].Price);
                    return;
                }
            }
            Console.WriteLine("There is not " + nameToSearch + " register as a product.");
        }
        public static void searchProductByManfDate(DateTime manfDateToSearch)
        {
            DateTime manfDateCurrentProduct;
            for (int i = 0; i < shopsProductsPD.Length; i++)
            {
                manfDateCurrentProduct = shopsProductsPD[i].ManufDate;
                if (DateTime.Compare(manfDateCurrentProduct, manfDateToSearch) == 0)
                {
                    Console.WriteLine("Product FOUND!");
                    Console.WriteLine(shopsProductsPD[i]); 
                    return;
                }
            }
            Console.WriteLine("There is not product register with "+manfDateToSearch.ToString("yyyy-MM-dd")+ " as a mufacturing date.");
        }
        public static bool printRegisteredProducts()
        {
            Console.WriteLine("ID \tNAME \t\tPRICE  \tManufactured \tExpire date \tExpired?");
            if (shopsProductsOL.Count == 0)
            {
                for(int i=0; i<shopsProductsPD.Length; i++)
                {
                    Console.WriteLine(shopsProductsPD[i]);
                }
                return true;
            }
            else
            {
                foreach(Product product in shopsProductsOL)
                {
                    Console.WriteLine(product);
                }
                return false;
            }
        }
        public static bool printRegisteredCostumers()
        {
            //Console.WriteLine("ID \tNAME \t\tPRICE  \tManufactured \tExpire date \tExpired?");
            if (shopsCostumers.Count == 0)
            {
                //for (int i = 0; i < shopsProductsPD.Length; i++)
                //{
                //    Console.WriteLine(shopsProductsPD[i]);
                //}
                return true;
            }
            else
            {
                foreach (Costumer costumer in shopsCostumers)
                {
                    Console.WriteLine(costumer);
                }
                return false;
            }
        }
        public static int getIndexByMenu(bool pPD, int pID)
        {
            if (pPD)
            {
                for (int i = 0; i < shopsProductsPD.Length; i++)
                {
                    if (shopsProductsPD[i].Id == pID)
                        return i;
                }
            }
            else
            {
                for (int i = 0; i < shopsProductsOL.Count; i++)
                {
                    if (shopsProductsPD[i].Id == pID)
                        return i;
                }
            }
            return -1;
        }
        public static void sortByExpiringdate(bool pPD)
        {
            if (pPD)
            {
                //DateTime currentDate, nextDate;
                for (int i = 0; i < shopsProductsPD.Length; i++)
                {
                    //currentDate = PD
                    if(i< shopsProductsPD.Length - 1)
                    {

                    }
                        
                }
            }
            else
            {
                shopsProductsOL.Sort();
            }
            Console.WriteLine("-> List of products now in order: ");
            printRegisteredProducts();
            
        }
        public static void sortCostumers()
        {
            //if (pPD)
            //{
            //    //DateTime currentDate, nextDate;
            //    for (int i = 0; i < shopsProductsPD.Length; i++)
            //    {
            //        //currentDate = PD
            //        if (i < shopsProductsPD.Length - 1)
            //        {

            //        }

            //    }
            //}
            //else
           //{
                ShopsCostumers.Sort();
            //}
            Console.WriteLine("-> List of costumer now in order: ");
            printRegisteredCostumers();

        }

        //Main
        public static void Main(string[] args){
            int answerMenu;
            Basket myBasket = new Basket();
            Console.WriteLine("Juan Gonzalez's shop");
            do
            {
                Console.WriteLine("MENU:");
                Console.WriteLine("1.- Register predefined products (5 items).");
                Console.WriteLine("2.- Register products manually (no limit).");
                Console.WriteLine("3.- Register predefined costumers (4 items).");
                Console.WriteLine("4.- Sort costumers by inversed email.");
                Console.WriteLine("5.- Print costumers registered on the shop.");
                Console.WriteLine("6.- Sort products by expiring date.");
                Console.WriteLine("7.- Print products registered on the shop.");
                Console.WriteLine("8.- Buy products to basket.");
                Console.WriteLine("9.- Print basket.");
                Console.WriteLine("10.- Search product PRICE from NAME.");
                Console.WriteLine("11- Search PRODUCT from MANUFACTURING DATA.");
                Console.WriteLine("12.- Exit.");
                answerMenu = int.Parse(Console.ReadLine());
                while(answerMenu < 1 || answerMenu > 12)
                {
                    Console.WriteLine("Invalid option, try again: ");
                    answerMenu = int.Parse(Console.ReadLine());
                }
                switch (answerMenu)
                {
                    case 1:
                        registerProductPD();
                        break;

                    case 2:
                        int counterP = 0, answ2;
                        do
                        {
                            Console.WriteLine("Enter the datails of the product on one line:");
                            Console.WriteLine("(ID,ProductName,Price,ExpireDate,ManufDate)");
                            Console.WriteLine("Example:");
                            Console.WriteLine("(123,Eggs,5,2021-11-30,2021-11-20)");
                            registerProductM();
                            counterP++;
                            Console.WriteLine("Do you want ro register other product? (0=no/1=yes)");
                            answ2 = int.Parse(Console.ReadLine());
                        } while (answ2 != 0);

                        break;
                    
                    case 3:
                        registerCostumerPD();
                        break;
                    
                    case 4:
                        //bool kindProducts = printRegisteredProducts();
                        sortCostumers();
                        break;
                    
                    case 5:
                        printRegisteredCostumers();
                        break;

                    case 6:
                        bool kindProducts = printRegisteredProducts();
                        sortByExpiringdate(kindProducts);
                        break;

                    case 7:
                        printRegisteredProducts();
                        break;

                    case 8:
                        int idP, index, quantityP;
                        bool productsPD = printRegisteredProducts();
                        Console.WriteLine("Select the product that you wanna boy: (ID)");
                        idP = int.Parse(Console.ReadLine());
                        index = getIndexByMenu(productsPD, idP);
                        while (index == -1)
                        {
                            Console.WriteLine("Invalid ID, try again: ");
                            idP = int.Parse(Console.ReadLine());
                            index = getIndexByMenu(productsPD, idP);
                        }
                        Console.WriteLine("Product to buy:");
                        Console.WriteLine("ID \tNAME \t\tPRICE  \tManufactured \tExpire date \tExpired?");
                        if (productsPD)
                            Console.WriteLine(shopsProductsPD[index]);
                        else
                            Console.WriteLine(shopsProductsOL[index]);
                        Console.WriteLine("insert the quantity of the prodcut tp buy: ");
                        quantityP = int.Parse(Console.ReadLine());
                        if (productsPD)
                        {
                            
                            myBasket.addProductToBasket(shopsProductsPD[index], quantityP);
                        }
                            
                        else
                        {
                            myBasket.addProductToBasket(shopsProductsOL[index], quantityP);
                        }
                        break;
                    
                    case 9:
                        myBasket.printBasket();
                        break;


                    case 10:
                        Console.WriteLine("introduce the name of the product to search:");
                        string name = Console.ReadLine();
                        searchPriceByName(name);
                        break;

                    case 11:
                        Console.WriteLine("introduce the Manufacture date of the product to search (YYYY-MM-DD):");
                        string date = Console.ReadLine();
                        string[] YMD = date.Split('-');
                        DateTime manufDateToSearch = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                        searchProductByManfDate(manufDateToSearch);
                        break;

                    case 12:
                        Environment.Exit(0);
                        break;
                }
            } while (answerMenu != 12);
            

        }
    }
}
