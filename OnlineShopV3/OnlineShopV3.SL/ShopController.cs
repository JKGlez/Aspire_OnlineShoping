using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OnlineShopV3.BO;
using OnlineShopV3.DL;

namespace OnlineShopV3.SL
{
    public class ShopController
    {
        public static ProductController theProductController = new ProductController();
        public static ProductDataAccess theProductDataAccess = new ProductDataAccess();

        public static InputValidation validInput = new InputValidation();

        private static List<Product> shopsProducts = new List<Product>();
        private static List<int> shopsProductsStock = new List<int>();

        private List<Employee> shopsEmployees = new List<Employee>();
        private List<Costumer> shopsCostumers = new List<Costumer>();


        //PROPIERTIES
        internal static List<Product> ShopsProducts { get => shopsProducts; set => shopsProducts = value; }
        public static List<int> ShopsProductsStock { get => shopsProductsStock; set => shopsProductsStock = value; }
        internal List<Employee> ShopsEmployees { get => shopsEmployees; set => shopsEmployees = value; }
        internal List<Costumer> ShopsCostumers { get => shopsCostumers; set => shopsCostumers = value; }

        public void ShopMenu()
        {
            Employee admin = new Employee("admin","admin@email.com", 1234567890,"street 0 number 0","admin",true);

            Console.WriteLine("Hola Mundo");
            //Reading the firsts products from XML file 
            theProductDataAccess.ReadProductsXML(shopsProducts,shopsProductsStock);
            EmployeesMenu(admin);
            //Product newProduct = theProductController.CreateProduct();
            //Console.WriteLine(newProduct);
            Console.ReadKey();
        }

        public static void PrintRegisteredProducts()
        {
            Console.WriteLine("ID \tCategory \tName \tPrice \tStock");
            for (int i = 0; i < ShopsProducts.Count; i++)
            {
                Console.WriteLine(ShopsProducts[i] + " " + ShopsProductsStock[i]);
            }
        }

        public static void EmployeesMenu(Employee theEmployee)
        {
            int answerE;
            do
            {
                Console.Clear();
                Console.WriteLine(" -> Employee MENU for " + theEmployee.Name + "<-");
                Console.WriteLine("1.- Print products registered on the shop.");
                Console.WriteLine("2.- Register a new product.");
                Console.WriteLine("3.- Add stock to a product.");
                Console.WriteLine("4.- Discontinue a product (admin required).");
                Console.WriteLine("5.- hire a new employee (admin required).");
                Console.WriteLine("6.- Fire a employee (admin required).");
                Console.WriteLine("7.- Print employees of the shop.");
                Console.WriteLine("8.- Return previous menu.");
                Console.WriteLine("9.- Exit program.");
                answerE = validInput.ValidatingNumberRange(0, 9);
                switch (answerE)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine(" -> Products registered on the shop <-");
                        PrintRegisteredProducts();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        Product newProduct = theProductController.RegisterProduct();
                        Console.WriteLine("Insert the stock for that product:.");
                        int stock = int.Parse(Console.ReadLine());
                        while (stock < 1)
                        {
                            Console.WriteLine("ERROR, Select a valid stock number:");
                            stock = int.Parse(Console.ReadLine());
                        }
                        shopsProducts.Add(newProduct);
                        ShopsProductsStock.Add(stock);
                        //To save the new product on the xml file
                        theProductDataAccess.WriteProductsXML(newProduct, stock);
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        //this.AddStock();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Clear();
                        if (theEmployee.AdminRights)
                        {
                            Console.WriteLine("Validating admin rights...ACCESS ACCEPTED");
                            Console.WriteLine("Press any key tocontinue...");
                            Console.ReadKey();
                            Console.Clear();
                            //this.DiscontinueProduct(theEmployee);
                        }
                        else
                        {
                            Console.WriteLine("Validating admin rights...ACCESS DENIED");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;


                    case 5:
                        Console.Clear();
                        if (theEmployee.AdminRights)
                        {
                            Console.WriteLine("Validating admin rights...ACCESS ACCEPTED");
                            Console.WriteLine("Press any key tocontinue...");
                            Console.ReadKey();
                            Console.Clear();
                            //this.HireEmployee(theEmployee);
                        }
                        else
                        {
                            Console.WriteLine("Validating admin rights...ACCESS DENIED");
                        }

                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.Clear();
                        if (theEmployee.AdminRights)
                        {
                            Console.WriteLine("Validating admin rights...ACCESS ACCEPTED");
                            Console.WriteLine("Press any key tocontinue...");
                            Console.ReadKey();
                            //this.FireEmployee(theEmployee);
                        }
                        else
                        {
                            Console.WriteLine("Validating admin rights...ACCESS DENIED");
                        }

                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine(" -> Employees registered on the shop <-");
                        //this.PrintRegisteredEmployees();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 8:
                        Console.Clear();
                        Console.WriteLine(" -> Returning previous menu <-");
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 9:
                        Console.Clear();
                        Console.WriteLine(" -> Exing program <-");
                        Console.WriteLine("Press any key exit...");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                }
            } while (answerE != 9);

        }
    }
}
