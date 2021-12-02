//Juan Gonzalez's CODE - task 1 (class) - 26/11/21
using System;
using System.Collections.Generic;


namespace OnlineShop{

   class Program
    {
        public static void Main(){
            Shop theShop = new Shop();
            int answerMenu;
            Basket myBasket = new Basket();
            Console.WriteLine("Juan Gonzalez's shop");
            do
            {
                Console.Clear();
                Console.WriteLine("MAIN MEMU:");
                Console.WriteLine("1.- Products options.");
                Console.WriteLine("2.- Costumers options.");
                Console.WriteLine("3.- Shop options.");
                Console.WriteLine("4.- Exit.");
                answerMenu = int.Parse(Console.ReadLine());
                while (answerMenu < 1 || answerMenu > 4)
                {
                    Console.WriteLine("Invalid option, try again: ");
                    answerMenu = int.Parse(Console.ReadLine());
                }
                switch (answerMenu)
                {
                    case 1:
                        int answerMenuP;
                        do
                        {
                            Console.WriteLine("PRODUCT MEMU:");
                            Console.WriteLine("1.- Register predefined products (5 items).");
                            Console.WriteLine("2.- Register products manually (no limit).");
                            Console.WriteLine("3.- Sort products by expiring date.");
                            Console.WriteLine("4.- Print products registered on the shop.");
                            Console.WriteLine("5.- Search product PRICE by NAME.");
                            Console.WriteLine("6.- Search PRODUCT from MANUFACTURING DATA.");
                            Console.WriteLine("7.- Return previous menu.");
                            answerMenuP = int.Parse(Console.ReadLine());
                            while (answerMenuP < 1 || answerMenuP > 7)
                            {
                                Console.WriteLine("Invalid option, try again: ");
                                answerMenuP = int.Parse(Console.ReadLine());
                            }
                            switch (answerMenuP)
                            {
                                case 1:
                                    theShop.RegisterProductPD();
                                    break;

                                case 2:
                                    int answ2;
                                    do
                                    {
                                        Console.WriteLine("Enter the datails of the product on one line:");
                                        Console.WriteLine("(ID,ProductName,Price,ExpireDate,ManufDate)");
                                        Console.WriteLine("Example:");
                                        Console.WriteLine("(123,Eggs,5,2021-11-30,2021-11-20)");
                                        theShop.RegisterProductM();
                                        Console.WriteLine("Do you want ro register other product? (0=no/1=yes)");
                                        answ2 = int.Parse(Console.ReadLine());
                                    } while (answ2 != 0);
                                    break;

                                case 3:
                                    bool kindProducts = theShop.PrintRegisteredProducts();
                                    theShop.SortByExpiringdate(kindProducts);
                                    Console.WriteLine("-> List of products now in order: ");
                                    theShop.PrintRegisteredProducts();
                                    break;

                                case 4:
                                    theShop.PrintRegisteredProducts();
                                    break;

                                case 5:
                                    Console.WriteLine("introduce the name of the product to search:");
                                    string name = Console.ReadLine();
                                    theShop.SearchPriceByName(name);
                                    break;

                                case 6:
                                    Console.WriteLine("introduce the Manufacture date of the product to search (YYYY-MM-DD):");
                                    string date = Console.ReadLine();
                                    string[] YMD = date.Split('-');
                                    DateTime manufDateToSearch = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                                    theShop.SearchProductByManfDate(manufDateToSearch);
                                    break;

                                case 7:

                                    break;
                            }
                        } while (answerMenuP != 7);
                        break;

                    case 2:
                        int answerMenuC;
                        do
                        {
                            Console.WriteLine("COSTUMER MEMU:");
                            Console.WriteLine("1.- Register predefined costumers (4 items).");
                            Console.WriteLine("2.- Register costumers manually (no limit).");
                            Console.WriteLine("3.- Sort costumers by inversed email.");
                            Console.WriteLine("4.- Print costumers registered on the shop.");
                            Console.WriteLine("5.- Return previous menu.");
                            answerMenuC = int.Parse(Console.ReadLine());
                            while (answerMenuC < 1 || answerMenuC > 5)
                            {
                                Console.WriteLine("Invalid option, try again: ");
                                answerMenuC = int.Parse(Console.ReadLine());
                            }
                            switch (answerMenuC)
                            {
                                case 1:
                                    theShop.RegisterCostumerPD();
                                    break;

                                case 2:
                                    int answ2;
                                    do
                                    {
                                        theShop.RegisterCostumerM();
                                        Console.WriteLine("Do you want ro register other costumer? (0=no/1=yes)");
                                        answ2 = int.Parse(Console.ReadLine());
                                    } while (answ2 != 0);
                                    break;

                                case 3:
                                    theShop.SortCostumers();
                                    break;

                                case 4:
                                    theShop.PrintRegisteredCostumers();
                                    break;

                                case 5:

                                    break;

                            }
                        } while (answerMenuC != 5);

                        break;

                    case 3:
                        int answerMenuS;
                        do
                        {
                            Console.WriteLine("SHOP MEMU:");
                            Console.WriteLine("1.- Buy products to basket.");
                            Console.WriteLine("2.- Print basket.");
                            Console.WriteLine("3.- Checkout.");
                            Console.WriteLine("4.- Return previous menu.");

                            answerMenuS = int.Parse(Console.ReadLine());
                            while (answerMenuS < 1 || answerMenuS > 4)
                            {
                                Console.WriteLine("Invalid option, try again: ");
                                answerMenuS = int.Parse(Console.ReadLine());
                            }
                            switch (answerMenuS)
                            {
                                case 1:
                                    int idP, index, quantityP;
                                    bool productsPD = theShop.PrintRegisteredProducts();
                                    Console.WriteLine("Select the product that you wanna boy: (ID)");
                                    idP = int.Parse(Console.ReadLine());
                                    index = theShop.GetIndexByMenu(productsPD, idP);
                                    while (index == -1)
                                    {
                                        Console.WriteLine("Invalid ID, try again: ");
                                        idP = int.Parse(Console.ReadLine());
                                        index = theShop.GetIndexByMenu(productsPD, idP);
                                    }
                                    Console.WriteLine("Product to buy:");
                                    Console.WriteLine("ID \tNAME \t\tPRICE  \tManufactured \tExpire date \tExpired?");
                                    if (productsPD)
                                        Console.WriteLine(theShop.ShopsProductsPD[index]);
                                    else
                                        Console.WriteLine(theShop.ShopsProductsOL[index]);
                                    Console.WriteLine("insert the quantity of the prodcut tp buy: ");
                                    quantityP = int.Parse(Console.ReadLine());
                                    if (productsPD)
                                    {

                                        myBasket.AddProductToBasket(theShop.ShopsProductsPD[index], quantityP);
                                    }

                                    else
                                    {
                                        myBasket.AddProductToBasket(theShop.ShopsProductsOL[index], quantityP);
                                    }
                                    break;

                                case 2:
                                    myBasket.PrintBasket();
                                    break;

                                case 3:
                                    theShop.SortCostumers();
                                    break;

                                case 4:
                                    theShop.PrintRegisteredCostumers();
                                    break;

                                case 5:
                                    Console.WriteLine("introduce the name of the product to search:");
                                    string name = Console.ReadLine();
                                    theShop.SearchPriceByName(name);
                                    break;

                            }
                        } while (answerMenuS != 4);
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;


                }


            } while (answerMenu != 4);
        }
    }
}
