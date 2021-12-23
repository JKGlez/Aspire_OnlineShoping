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
        //Instances of SL
        public static ProductController theProductController = new ProductController();
        public static EmployeeController theEmployeeController = new EmployeeController();

        //Instances of DL
        public static ProductDataAccess theProductDataAccess = new ProductDataAccess();
        public static EmployeeDataAccess theEmployeeDataAccess = new EmployeeDataAccess();

        //EXCEPTION HANDLING
        public static InputValidation validInput = new InputValidation();

        //FIELDS
        private static List<Product> shopsProducts = new List<Product>();
        private static List<int> shopsProductsStock = new List<int>();
        private static List<Employee> shopsEmployees = new List<Employee>();
        private static List<Costumer> shopsCostumers = new List<Costumer>();


        //PROPIERTIES
        internal static List<Product> ShopsProducts { get => shopsProducts; set => shopsProducts = value; }
        public static List<int> ShopsProductsStock { get => shopsProductsStock; set => shopsProductsStock = value; }
        internal static  List<Employee> ShopsEmployees { get => shopsEmployees; set => shopsEmployees = value; }
        internal static List<Costumer> ShopsCostumers { get => shopsCostumers; set => shopsCostumers = value; }

        public void ShopMenu()
        {
            //Employee admin = new Employee("admin","admin@email.com", 1234567890,"street 0 number 0","admin",true);
            //theEmployeeController.HireEmployee(admin,shopsEmployees);
            //Console.WriteLine("Hola Mundo");
            //Reading the firsts products from XML file 
            //theProductDataAccess.ReadProductsXML(shopsProducts,shopsProductsStock);
            theProductDataAccess.ReadProductsDB(shopsProducts, shopsProductsStock);
            theEmployeeDataAccess.ReadEmployeesDB(shopsEmployees);
            int answerC = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(" -> LOGIN FOR USER <-");
                Console.WriteLine("Select your user'profile: ");
                Console.WriteLine("1.- Employee.");
                Console.WriteLine("2.- Costumer.");

                answerC = validInput.ValidatingNumberRange(1, 2);
                switch (answerC)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine(" -> Login for Employees <-");
                        Console.WriteLine("Select your Employee'profile: ");
                        PrintRegisteredEmployees();
                        int idEmployee = validInput.ValidatingNumberRange(0, ShopsEmployees.Count);
                        Console.WriteLine("Insert the passwod for: " + ShopsEmployees[idEmployee].Name);
                        if (validInput.ValidatingPassword(ShopsEmployees[idEmployee]))
                        {
                            Employee userE = ShopsEmployees[idEmployee];
                            EmployeesMenu(userE);
                        }
                        else
                        {

                        }
                        break;
                }
            } while (answerC != 0);
         
            //return null;

            //EmployeesMenu(admin);
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

        public static void PrintRegisteredEmployees()
        {
            Console.WriteLine("ID \tName");
            for (int i = 0; i < ShopsEmployees.Count; i++)
            {
                Console.WriteLine(i + "\t" + ShopsEmployees[i].Name);
            }
        }

        //CHECK HOW UPDATE ON THE DB, XML AND TXT
        public void AddStock()
        {
            Console.WriteLine("Select the ID of the product to increase stock:");
            PrintRegisteredProducts();
            int answerID = validInput.ValidatingID(ShopsProducts);
            for (int i = 0; i < ShopsProducts.Count; i++)
            {
                if (ShopsProducts[i].Id == answerID)
                {
                    Console.WriteLine("The product to add stock is:");
                    Console.WriteLine(ShopsProducts[i]);
                    Console.WriteLine("Select to quantity of product to add on the stock:");
                    int amountAdd = int.Parse(Console.ReadLine());
                    ShopsProductsStock[i] += amountAdd;
                    Console.WriteLine("Stock added successfully!");
                }
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
                Console.WriteLine("3.- Modify exiting product.");
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
                        Product newProduct = theProductController.CreateProduct();
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
                        //theProductDataAccess.WriteProductsXML(newProduct, stock);
                        int resDB = theProductController.RegisterProduct(newProduct, stock);
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine(" -> Modify a product on the shop <-");
                        Console.WriteLine("Registered products:");
                        PrintRegisteredProducts();
                        theProductController.UpdateProductInfo(theEmployee, ShopsProducts, ShopsProductsStock);
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
                            Console.WriteLine(" -> Descontinue a product on the shop <-");
                            Console.WriteLine("Registered products:");
                            PrintRegisteredProducts();
                            theProductController.DiscontinueProduct(theEmployee, ShopsProducts, ShopsProductsStock);
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
                            theEmployeeController.HireEmployee(theEmployee,ShopsEmployees);
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
                            Console.WriteLine(" -> Fire employee for the shop <-");
                            PrintRegisteredEmployees();
                            theEmployeeController.FireEmployee(theEmployee,ShopsEmployees);
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
                        PrintRegisteredEmployees();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 8:
                        Console.Clear();
                        Console.WriteLine(" -> Returning previous menu <-");
                        Console.WriteLine("Press any key tocontinue...");
                        return;
                        

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
