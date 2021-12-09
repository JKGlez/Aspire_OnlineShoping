using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspire_OnlineShop_V2._0
{
    class Shop
    {
        //FIELDS
        InputValidation validInput = new InputValidation();

        private List<Product> shopsProducts = new List<Product>();
        private List<int> shopsProductsStock = new List<int>();

        private List<Employee> shopsEmployees = new List<Employee>();
        private List<Costumer> shopsCostumers = new List<Costumer>();
        
        //PROPIERTIES
        internal List<Product> ShopsProducts { get => shopsProducts; set => shopsProducts = value; }
        public List<int> ShopsProductsStock { get => shopsProductsStock; set => shopsProductsStock = value; }
        internal List<Employee> ShopsEmployees { get => shopsEmployees; set => shopsEmployees = value; }
        internal List<Costumer> ShopsCostumers { get => shopsCostumers; set => shopsCostumers = value; }

        public void RegisterProductPD()
        {
            //GROCERIES PD
            int[] idsG = { 123, 234, 345, 567, 678 };
            string[] namesG = { "Milk", "Eggs", "Flour", "Ham", "Bread" };
            double[] pricesG = { 25.00, 5.00, 20.00, 37.50, 45.50 };
            DateTime[] expireDatesG = { new DateTime(2021, 12, 10), new DateTime(2021, 11, 21), new DateTime(2022, 02, 01), new DateTime(2021, 11, 25), new DateTime(2021, 12, 15) };
            DateTime[] manufDatesG = { new DateTime(2021, 11, 01), new DateTime(2021, 11, 03), new DateTime(2021, 08, 25), new DateTime(2021, 11, 08), new DateTime(2021, 11, 05) };
            for (int i = 0; i < idsG.Length; i++)
            {
                ShopsProducts.Add(new Grocery(idsG[i], "Groceries", namesG[i], pricesG[i], expireDatesG[i], manufDatesG[i]));
                ShopsProductsStock.Add(100);
            }
            //ELECTRONIC PD
            int[] idsE = { 987, 876, 765, 654, 543 };
            string[] namesE = { "Fridge", "Laptop", "Phone", "TV", "Tablet" };
            double[] pricesE = { 5000.00, 7500.00, 3500.00, 10000.00, 2500.00 };
            DateTime[] guaranteeE = { new DateTime(2022, 12, 12), new DateTime(2022, 11, 11), new DateTime(2022, 10, 10), new DateTime(2021, 09, 09), new DateTime(2021, 08, 08)};
            for (int i = 0; i < idsE.Length; i++)
            {
                ShopsProducts.Add(new Electronic(idsE[i], "Electronic", namesE[i], pricesE[i], guaranteeE[i]));
                ShopsProductsStock.Add(10);
            }
            Console.WriteLine("Predefined products registered successfully! (10/10)");
        }
        public void RegisterEmployeesPD()
        {
            string[] namesC = {"Jose Hernandez", "David Zuniga" };
            string[] emailC = { "jose.hernandez@email.com", "david.zuniga@email.com" };
            long[] mobileC = {3456789012, 4567890123 };
            string[] addressC = {"street 3 number 3", "street 4 number 4" };
            string[] passC = {"jose", "david" };
            double[] walletC= {150.50,1100.50};
            for (int i = 0; i < namesC.Length; i++)
            {
                ShopsCostumers.Add(new Costumer(namesC[i], emailC[i], mobileC[i], addressC[i], passC[i], walletC[i]));
            }
            Console.WriteLine("Predefined costumer registered successfully! (2/2)");
        }

        public void RegisterCostumersPD()
        {

            //ELECTRONIC PD
            string[] namesE = { "admin", "Juan Gonzalez", "Ercik Balandas"};
            string[] emailE = { "admin@email.com", "juan.jonzalez@email.com", "ercik.balandas@email.com"};
            long[] mobileE = { 0123456789, 1234567890, 2345678901, 3456789012, 4567890123 };
            string[] addressE = { "street 0 number 0", "street 1 number 1", "street 2 number 2"};
            string[] passE = { "admin", "juan", "erick"};
            bool[] adminRightsE = { true, false, false};
            for (int i = 0; i < namesE.Length; i++)
            {
                ShopsEmployees.Add(new Employee(namesE[i], emailE[i], mobileE[i], addressE[i], passE[i], adminRightsE[i]));
            }
            Console.WriteLine("Predefined employees registered successfully! (3/3)");
        }
        public void RegisterProduct()
        {
            string product;
            string[] productData;
            string productName;

            Console.WriteLine("\t -> REGISTERING PRODUCT <-");
            Console.WriteLine("Select product department:");
            Console.WriteLine("1.-Groceries.");
            Console.WriteLine("2.-Electronics.");
            int category = int.Parse(Console.ReadLine());
            while (category < 1 || category > 2)
            {
                Console.WriteLine("ERROR, Select a valid department:");
                category = int.Parse(Console.ReadLine());
            }
            switch (category)
            {
                case 1:
                    DateTime productManufacturingDate;
                    DateTime productExpiringDate;
                    Console.Clear();
                    Console.WriteLine("\t -> REGISTERING A GROCERY <-");
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
                    Product currentProduct = new Grocery(productId, "Groceries", productName, productPrice, productExpiringDate, productManufacturingDate);
                    ShopsProducts.Add(currentProduct);
                    Console.WriteLine("Insert the stock for that product:.");
                    int stock = int.Parse(Console.ReadLine());
                    while (stock < 1)
                    {
                        Console.WriteLine("ERROR, Select a valid stock number:");
                        stock = int.Parse(Console.ReadLine());
                    }
                    ShopsProductsStock.Add(stock);
                    Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
                    Console.WriteLine(currentProduct.PrintProduct());
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("\t -> REGISTERING AN ELECTRONIC <-");
                    Console.WriteLine("Enter the datails of the product on one line:");
                    Console.WriteLine("(ID,ProductName,Price,Guarantee)");
                    Console.WriteLine("Example:");
                    Console.WriteLine("(123,Fridge,2500,2022-12-12)");
                    product = Console.ReadLine();
                    productData = product.Split(',');
                    int.TryParse(productData[0], out productId);
                    productName = productData[1];
                    double.TryParse(productData[2], out productPrice);
                    YMD = productData[3].Split('-');
                    DateTime productGuarantee = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                    currentProduct = new Electronic(productId, "Electronic", productName, productPrice, productGuarantee);
                    ShopsProducts.Add(currentProduct);
                    Console.WriteLine("Insert the stock for that product:.");
                    stock = int.Parse(Console.ReadLine());
                    while (stock < 1)
                    {
                        Console.WriteLine("ERROR, Select a valid stock number:");
                        stock = int.Parse(Console.ReadLine());
                    }
                    ShopsProductsStock.Add(stock);
                    Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
                    Console.WriteLine(currentProduct.PrintProduct());
                    break;
            }
        }
        public void AddStock()
        {
            Console.WriteLine("Select the ID of the product to increase stock:");
            this.PrintRegisteredProducts();
            int answerID = validInput.ValidatingID(ShopsProducts);
            for(int i = 0; i < ShopsProducts.Count; i++)
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
        public void DiscontinueProduct(Employee theEmployee)
        {
            Console.WriteLine(" -> Descontinue a product on the shop <-");
            Console.WriteLine("Registered products:");
            this.PrintRegisteredProducts();
            Console.WriteLine("Select the ID of the product to discotinue:");
            
            Product selectedProduct = validInput.ValidatingProdcutByID(this.ShopsProducts);
            
            Console.WriteLine("The product to discontinue is:");
            Console.WriteLine(selectedProduct);
            Console.WriteLine("continue?: (yes/no)");
            if (validInput.ValidatingYesNo().Equals("yes"))
            {
                Console.WriteLine("Insert admin password ("+theEmployee.Name+"): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                    Console.WriteLine("Password accepted, Product removed...");
                    int index = ShopsProducts.IndexOf(selectedProduct);
                    ShopsProducts.Remove(selectedProduct);
                    ShopsProductsStock.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Invalid password, canceling action");
                }
            }
            else
            {
                Console.WriteLine("Canceling action...");
            }
        }
        public void HireEmployee(Employee theEmployee)
        {
            Console.WriteLine(" -> Hire employee for the shop <-");
            Console.WriteLine("Insert the data of the new employee on one line:");
            Console.WriteLine("(name,email,mobile,address,password,adminRights)");
            Console.WriteLine("example:");
            Console.WriteLine("Pedro,pedro@email.com,9876543210,street 10 number 10,pedro,true");
            Employee hiredEmployee = validInput.ValidatingNewEmployee();
            Console.WriteLine("New employee data:");
            Console.WriteLine(hiredEmployee);
            Console.WriteLine("continue?: (yes/no)");
            if (validInput.ValidatingYesNo().Equals("yes"))
            {
                Console.WriteLine("Insert admin password (" + theEmployee.Name + "): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                    Console.WriteLine("Password accepted, Employee hired...");
                    ShopsEmployees.Add(hiredEmployee);
                }
                else
                {
                    Console.WriteLine("Invalid password, canceling action");
                }
            }
            else
            {
                Console.WriteLine("Canceling action...");
            }

        }
        public void FireEmployee(Employee theEmployee)
        {
            Console.WriteLine(" -> Fire employee for the shop <-");
            PrintRegisteredEmployees();
            Console.WriteLine("Select the number of the employee to fire:");
            Employee firedEmployee = validInput.ValidatingExitedEmployee(ShopsEmployees);
            Console.WriteLine("Employee to fire info::");
            Console.WriteLine("Name \tEmail \tMobile");
            Console.WriteLine(firedEmployee);
            Console.WriteLine("continue?: (yes/no)");
            if (validInput.ValidatingYesNo().Equals("yes"))
            {
                Console.WriteLine("Insert admin password (" + theEmployee.Name + "): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                    Console.WriteLine("Password accepted, Employee fired...");
                    ShopsEmployees.Remove(firedEmployee);
                }
                else
                {
                    Console.WriteLine("Invalid password, canceling action");
                }
            }
            else
            {
                Console.WriteLine("Canceling action...");
            }

        }
        public Employee LoginE()
        {
            Console.WriteLine(" -> Login for Employees <-");
            Console.WriteLine("Select your Employee'profile: ");
            PrintRegisteredEmployees();    
            int idEmployee = validInput.ValidatingNumberRange(0, ShopsEmployees.Count);
            Console.WriteLine("Insert the passwod for: " + ShopsEmployees[idEmployee].Name);
            if (validInput.ValidatingPassword(ShopsEmployees[idEmployee]))
            {
                return ShopsEmployees[idEmployee];
            }
            else
            {
                return null;
            }
        }
        public Costumer LoginC()
        {
            Console.WriteLine(" -> Login for Costumers <-");
            Console.WriteLine("Select your costumer'profile: ");
            Console.WriteLine("1.- Registered costumer.");
            Console.WriteLine("2.- New costumer.");

            int answerC = validInput.ValidatingNumberRange(1, 2);
            switch (answerC)
            {
                case 1:
                    Console.WriteLine(" -> Costumers registered <-");
                    PrintRegisteredCostumers();
                    break;


            }
            return null;
        }

        public void EmployeesMenu(Employee theEmployee)
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
                        this.PrintRegisteredProducts();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;
                    
                    case 2:
                        Console.Clear();
                        this.RegisterProduct();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        this.AddStock();
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
                            this.DiscontinueProduct(theEmployee);
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
                            this.HireEmployee(theEmployee);
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
                            this.FireEmployee(theEmployee);
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
                        this.PrintRegisteredEmployees();
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

        public void CostumerMenu(Costumer theCostumer)
        {
            int answerE;
            do
            {
                Console.Clear();
                Console.WriteLine(" -> Employee MENU for " + theCostumer.Name + "<-");
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
                        this.PrintRegisteredProducts();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        this.RegisterProduct();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        this.AddStock();
                        Console.WriteLine("Press any key tocontinue...");
                        Console.ReadKey();
                        break;

                    case 4:
                        //Console.Clear();
                        //if (theCostumer.AdminRights)
                        //{
                        //    Console.WriteLine("Validating admin rights...ACCESS ACCEPTED");
                        //    Console.WriteLine("Press any key tocontinue...");
                        //    Console.ReadKey();
                        //    Console.Clear();
                        //    this.DiscontinueProduct(theCostumer);
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Validating admin rights...ACCESS DENIED");
                        //}

                        //Console.WriteLine("Press any key to continue...");
                        //Console.ReadKey();
                        break;


                    case 5:
                        //Console.Clear();
                        //if (theCostumer.AdminRights)
                        //{
                        //    Console.WriteLine("Validating admin rights...ACCESS ACCEPTED");
                        //    Console.WriteLine("Press any key tocontinue...");
                        //    Console.ReadKey();
                        //    Console.Clear();
                        //    this.HireEmployee(theCostumer);
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Validating admin rights...ACCESS DENIED");
                        //}

                        //Console.WriteLine("Press any key tocontinue...");
                        //Console.ReadKey();
                        break;

                    case 6:
                        //Console.Clear();
                        //if (theCostumer.AdminRights)
                        //{
                        //    Console.WriteLine("Validating admin rights...ACCESS ACCEPTED");
                        //    Console.WriteLine("Press any key tocontinue...");
                        //    Console.ReadKey();
                        //    this.FireEmployee(theCostumer);
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Validating admin rights...ACCESS DENIED");
                        //}

                        //Console.WriteLine("Press any key to continue...");
                        //Console.ReadKey();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine(" -> Employees registered on the shop <-");
                        this.PrintRegisteredEmployees();
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



        public void PrintRegisteredProducts()
        {
            Console.WriteLine("ID \tCategory \tName \tPrice \tStock");
            for(int i = 0; i < ShopsProducts.Count; i++)
            {
                Console.WriteLine(ShopsProducts[i] + " " + ShopsProductsStock[i]);
            }
        }

        public void PrintRegisteredEmployees()
        {
            Console.WriteLine("ID \tName");
            for (int i = 0; i < ShopsEmployees.Count; i++)
            {
                Console.WriteLine(i + "\t" + ShopsEmployees[i].Name);
            }
        }

        public void PrintRegisteredCostumers()
        {
            Console.WriteLine("ID \tName");
            for (int i = 0; i < ShopsCostumers.Count; i++)
            {
                Console.WriteLine(i + "\t" + ShopsCostumers[i].Name);
            }
        }

    }
}
