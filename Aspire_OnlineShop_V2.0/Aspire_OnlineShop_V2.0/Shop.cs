using System;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Xml;
using System.Xml.Linq;

namespace Aspire_OnlineShop_V2._0
{
    class Shop
    {
        //FIELDS
        InputValidation validInput = new InputValidation();
        string filePathP = @"C:\Users\jk\Documents\Aspire\Aspire_OnlineShop_V2.0\Files\Products.txt";
        string filePathE = @"C:\Users\jk\Documents\Aspire\Aspire_OnlineShop_V2.0\Files\Employees.txt";



        private List<Product> shopsProducts = new List<Product>(); 
        private List<int> shopsProductsStock = new List<int>();

        private List<Employee> shopsEmployees = new List<Employee>();
        private List<Costumer> shopsCostumers = new List<Costumer>();

        //PROPIERTIES
        internal List<Product> ShopsProducts { get => shopsProducts; set => shopsProducts = value; }
        public List<int> ShopsProductsStock { get => shopsProductsStock; set => shopsProductsStock = value; }
        internal List<Employee> ShopsEmployees { get => shopsEmployees; set => shopsEmployees = value; }
        internal List<Costumer> ShopsCostumers { get => shopsCostumers; set => shopsCostumers = value; }

        /*Products Methods*/
        public void RegisterProductPD()
        {
            //ReadProductsTXT();
            ReadProductsXML();
        }
        public void ReadProductsTXT()
        {
            Stream fileP = new FileStream(filePathP, FileMode.Open, FileAccess.Read);
            StreamReader readerP = new StreamReader(fileP);
            Product currentProduct = null;
            int productStock = 0;
            while (!readerP.EndOfStream)
            {
                string product = readerP.ReadLine();
                if (!string.IsNullOrEmpty(product))
                {
                    string[] productData = product.Split(',');
                    int.TryParse(productData[0], out int productId);
                    string productType = productData[1];
                    string productName = productData[2];
                    if (productType.ToLower().Equals("groceries"))
                    {
                        double.TryParse(productData[3], out double productPrice);
                        productStock = int.Parse(productData[4]);
                        string[] YMD = productData[5].Split('/');
                        DateTime productExpiringDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                        YMD = productData[6].Split('/');
                        DateTime productManufacturingDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                        currentProduct = new Grocery(productId, productType, productName, productPrice, productExpiringDate, productManufacturingDate);
                        ShopsProducts.Add(currentProduct);
                        ShopsProductsStock.Add(productStock);
                    }
                    else if (productType.ToLower().Equals("electronics"))
                    {
                        double.TryParse(productData[3], out double productPrice);
                        productStock = int.Parse(productData[4]);
                        ShopsProductsStock.Add(productStock);
                        string[] YMD = productData[5].Split('/');
                        DateTime productGuarantee = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                        currentProduct = new Electronic(productId, productType, productName, productPrice, productGuarantee);
                        ShopsProducts.Add(currentProduct);
                    }
                }
                WriteProductsXML(currentProduct,productStock);
            }
            
            fileP.Close();
            readerP.Close();
            Console.WriteLine("Predefined products registered successfully!");
        }
        public void WriteProductTXT(string product)
        {
            StreamWriter writerP = File.AppendText(filePathP);

            writerP.WriteLine(product);

            writerP.Close();
        }
        public void ReadProductsXML()
        {
            XmlDocument xmlDocProducts = new XmlDocument();
            try
            {
                xmlDocProducts.Load("./productsXML.xml");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The XML with the data was not found...creating file");
                XmlNode rootNode = xmlDocProducts.CreateElement("ProductsOnTheShop");
                xmlDocProducts.AppendChild(rootNode);
                XmlDeclaration declaration = xmlDocProducts.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocProducts.InsertBefore(declaration, rootNode);
                xmlDocProducts.Save("productsXML.xml");
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The XML with the data is empty...creating format");
                XmlNode rootNode = xmlDocProducts.CreateElement("ProductsOnTheShop");
                xmlDocProducts.AppendChild(rootNode);
                XmlDeclaration declaration = xmlDocProducts.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocProducts.InsertBefore(declaration, rootNode);
                xmlDocProducts.Save("productsXML.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                xmlDocProducts.Load("./productsXML.xml");
                Product currentProduct = null;
                //Console.WriteLine(xmlDocProducts.InnerXml);
                //Console.ReadKey();

                int productId = 0, productStock = 0; ;
                string productName = "";
                double productPrice = 0;
                DateTime productExpiringDate = new DateTime();
                DateTime productManufacturingDate = new DateTime();
                DateTime productGuarantee = new DateTime();
                string[] dateS;

                var ProductsOnTheShop = xmlDocProducts.DocumentElement.ChildNodes;
                foreach (XmlNode productNode in ProductsOnTheShop)
                {
                    if (productNode.HasChildNodes)
                    {
                        //Console.WriteLine(productNode.InnerXml);
                        foreach (XmlNode field in productNode.ChildNodes)
                        {
                            //Console.WriteLine(field.Name + " -> " + field.InnerText);
                            if (productNode.Attributes["Category"].Value.ToLower().Equals("groceries"))
                            {
                                switch (field.Name)
                                {
                                    case "ID":
                                        productId = int.Parse(field.InnerText);
                                        break;
                                    case "Name":
                                        productName = field.InnerText;
                                        break;
                                    case "Price":
                                        productPrice = double.Parse(field.InnerText);
                                        break;
                                    case "Stock":
                                        productStock = int.Parse(field.InnerText);
                                        break;
                                    case "Expire_Date":
                                        dateS = field.InnerText.Split('/');
                                        productExpiringDate = new DateTime(int.Parse(dateS[0]), int.Parse(dateS[1]), int.Parse(dateS[2]));
                                        break;
                                    case "Manuf_Date":
                                        dateS = field.InnerText.Split('/');
                                        productManufacturingDate = new DateTime(int.Parse(dateS[0]), int.Parse(dateS[1]), int.Parse(dateS[2]));
                                        break;
                                }
                            }

                            if (productNode.Attributes["Category"].Value.ToLower().Equals("electronics"))
                            {
                                switch (field.Name)
                                {
                                    case "ID":
                                        productId = int.Parse(field.InnerText);
                                        break;
                                    case "Name":
                                        productName = field.InnerText;
                                        break;
                                    case "Price":
                                        productPrice = double.Parse(field.InnerText);
                                        break;
                                    case "Stock":
                                        productStock = int.Parse(field.InnerText);
                                        break;
                                    case "Guarantee_Date":
                                        dateS = field.InnerText.Split('/');
                                        productGuarantee = new DateTime(int.Parse(dateS[0]), int.Parse(dateS[1]), int.Parse(dateS[2]));
                                        break;
                                }
                            }
                        }
                        if (productNode.Attributes["Category"].Value.ToLower().Equals("groceries"))
                        {
                            Grocery grocery = new Grocery(productId, "groceries", productName, productPrice, productExpiringDate, productManufacturingDate);
                            ShopsProducts.Add(grocery);
                            ShopsProductsStock.Add(productStock);
                        }
                        if (productNode.Attributes["Category"].Value.ToLower().Equals("electronics"))
                        {
                            Electronic electronic = new Electronic(productId, "electronics", productName, productPrice, productGuarantee);
                            ShopsProducts.Add(electronic);
                            ShopsProductsStock.Add(productStock);
                        }
                    }

                }
            }
        }
        public void WriteProductsXML(Product theProduct, int theStock)
        {
            XmlDocument xmlDocProducts = new XmlDocument();
            try
            {
                xmlDocProducts.Load("./productsXML.xml");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The XML with the data was not found...creating file");
                XmlNode rootNode = xmlDocProducts.CreateElement("ProductsOnTheShop");
                xmlDocProducts.AppendChild(rootNode);
                XmlDeclaration declaration = xmlDocProducts.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocProducts.InsertBefore(declaration, rootNode);
                xmlDocProducts.Save("productsXML.xml");
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The XML with the data is empty...creating format");
                XmlNode rootNode = xmlDocProducts.CreateElement("ProductsOnTheShop");
                xmlDocProducts.AppendChild(rootNode);
                XmlDeclaration declaration = xmlDocProducts.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocProducts.InsertBefore(declaration, rootNode);
                xmlDocProducts.Save("productsXML.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                //Assigned the "Root Node" of a existing xml file.
                XmlNode rootNode = xmlDocProducts.SelectSingleNode("ProductsOnTheShop");
                //PRODUCT
                XmlNode productNode = xmlDocProducts.CreateElement("Product");

                ////Example attribute
                //XmlAttribute productAttribute = xmlDocProducts.CreateAttribute("ExampleOfAttribute");
                //productAttribute.Value = "Example of Attribute";
                //productNode.Attributes.Append(productAttribute);

                //Example attribute
                XmlAttribute productCategory = xmlDocProducts.CreateAttribute("Category");
                productCategory.Value = theProduct.Category;
                productNode.Attributes.Append(productCategory);

                //ID
                XmlNode productId = xmlDocProducts.CreateElement("ID");
                XmlText prductIdValue = xmlDocProducts.CreateTextNode(theProduct.Id.ToString());
                productId.AppendChild(prductIdValue);
                productNode.AppendChild(productId);

                ////Category
                //XmlNode productCategory = xmlDocProducts.CreateElement("Category");
                //XmlText prductCategoryValue = xmlDocProducts.CreateTextNode(theProduct.Category);
                //productCategory.AppendChild(prductCategoryValue);
                //productNode.AppendChild(productCategory);

                //Name
                XmlNode productName = xmlDocProducts.CreateElement("Name");
                XmlText prductNameValue = xmlDocProducts.CreateTextNode(theProduct.Name);
                productName.AppendChild(prductNameValue);
                productNode.AppendChild(productName);

                //Price
                XmlNode productPrice = xmlDocProducts.CreateElement("Price");
                XmlText prductPriceValue = xmlDocProducts.CreateTextNode(theProduct.Price.ToString());
                productPrice.AppendChild(prductPriceValue);
                productNode.AppendChild(productPrice);

                //Stock
                XmlNode productStock = xmlDocProducts.CreateElement("Stock");
                XmlText prductStockValue = xmlDocProducts.CreateTextNode(theStock.ToString());
                productStock.AppendChild(prductStockValue);
                productNode.AppendChild(productStock);

                //Dates
                if (theProduct.GetType().ToString().Equals("Aspire_OnlineShop_V2._0.Grocery"))
                {
                    //Casting Product to Grocerie
                    Grocery productG = (Grocery)theProduct;
                    //ExpDate
                    XmlNode productExpDate = xmlDocProducts.CreateElement("Expire_Date");
                    XmlText prductExpDateValue = xmlDocProducts.CreateTextNode(productG.ExpireDate.ToString("yyyy/MM/dd"));
                    productExpDate.AppendChild(prductExpDateValue);
                    productNode.AppendChild(productExpDate);
                    //ManufDate
                    XmlNode productManufDate = xmlDocProducts.CreateElement("Manuf_Date");
                    XmlText prductManufDateValue = xmlDocProducts.CreateTextNode(productG.ManufDate.ToString("yyyy/MM/dd"));
                    productManufDate.AppendChild(prductManufDateValue);
                    productNode.AppendChild(productManufDate);
                }
                else if (theProduct.GetType().ToString().Equals("Aspire_OnlineShop_V2._0.Electronic"))
                {
                    //Casting Product to Electronic
                    Electronic productE = (Electronic)theProduct;
                    //GuaranteeDate
                    XmlNode productGuarateeDate = xmlDocProducts.CreateElement("Guarantee_Date");
                    XmlText prductGuarateeDateValue = xmlDocProducts.CreateTextNode(productE.Guarantee.ToString("yyyy/MM/dd"));
                    productGuarateeDate.AppendChild(prductGuarateeDateValue);
                    productNode.AppendChild(productGuarateeDate);
                }
                rootNode.AppendChild(productNode);
                xmlDocProducts.Save("./productsXML.xml");
            }
        }
        public void RegisterProduct()
        {
            string product;
            string s = "";
            string[] productData;
            string productName;
            int stock = 0;
            Product currentProduct;




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
                    Console.WriteLine("(123,Eggs,5,2021/11/30,2021/11/20)");
                    product = Console.ReadLine();
                    productData = product.Split(',');
                    int.TryParse(productData[0], out int productId);
                    productName = productData[1];
                    double.TryParse(productData[2], out double productPrice);
                    string[] YMD = productData[3].Split('/');
                    productExpiringDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                    YMD = productData[4].Split('/');
                    productManufacturingDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                    currentProduct = new Grocery(productId, "Groceries", productName, productPrice, productExpiringDate, productManufacturingDate);
                    ShopsProducts.Add(currentProduct);
                    Console.WriteLine("Insert the stock for that product:.");
                    stock = int.Parse(Console.ReadLine());
                    while (stock < 1)
                    {
                        Console.WriteLine("ERROR, Select a valid stock number:");
                        stock = int.Parse(Console.ReadLine());
                    }
                    ShopsProductsStock.Add(stock);
                    s = (productId + ",Groceries," + productName + "," + productPrice + "," + stock + "," + productExpiringDate.ToString("yyyy/MM/dd") + "," + productManufacturingDate.ToString("yyyy/MM/dd"));
                    Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
                    Console.WriteLine(currentProduct.PrintProduct());
                    WriteProductsXML(currentProduct, stock);
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("\t -> REGISTERING AN ELECTRONIC <-");
                    Console.WriteLine("Enter the datails of the product on one line:");
                    Console.WriteLine("(ID,ProductName,Price,Guarantee)");
                    Console.WriteLine("Example:");
                    Console.WriteLine("(123,Fridge,2500,2022/12/12)");
                    product = Console.ReadLine();
                    productData = product.Split(',');
                    int.TryParse(productData[0], out productId);
                    productName = productData[1];
                    double.TryParse(productData[2], out productPrice);
                    YMD = productData[3].Split('/');
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
                    s = (productId + ",Electronics," + productName + "," + productPrice + "," + stock + "," + productGuarantee.ToString("yyyy/MM/dd"));
                    Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
                    Console.WriteLine(currentProduct.PrintProduct());
                    WriteProductsXML(currentProduct, stock);
                    break;
            }
            //WriteProductTXT(s);
            

        }
        public void AddStock()
        {
            Console.WriteLine("Select the ID of the product to increase stock:");
            this.PrintRegisteredProducts();
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
                Console.WriteLine("Insert admin password (" + theEmployee.Name + "): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                    Console.WriteLine("Password accepted, Product removed...");
                    int index = ShopsProducts.IndexOf(selectedProduct);
                    ShopsProducts.Remove(selectedProduct);
                    ShopsProductsStock.RemoveAt(index);
                    XmlDocument xmlProductsDoc = new XmlDocument();
                    xmlProductsDoc.Load("./productsXML.xml");
                    XmlNode rootProducts = xmlProductsDoc.DocumentElement;
                    XmlNodeList productList = xmlProductsDoc.SelectNodes("ProductsOnTheShop/Product");
                    foreach (XmlNode item in productList)
                    {
                        if (item.SelectSingleNode("ID").InnerText.Equals(selectedProduct.Id.ToString()))
                        {
                            XmlNode productRemove = item;
                            rootProducts.RemoveChild(productRemove);
                            xmlProductsDoc.Save("./productsXML.xml");
                        }
                    }
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
        public void PrintRegisteredProducts()
        {
            Console.WriteLine("ID \tCategory \tName \tPrice \tStock");
            for (int i = 0; i < ShopsProducts.Count; i++)
            {
                Console.WriteLine(ShopsProducts[i] + " " + ShopsProductsStock[i]);
            }
        }


        /*Employees Methods*/
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
        public void RegisterEmployeesPD()
        {

            ReadEmployeesTXT();
        }
        public void ReadEmployeesTXT()
        {
            Stream fileE = new FileStream(filePathE, FileMode.Open, FileAccess.Read);
            StreamReader readerE = new StreamReader(fileE);
            while (!readerE.EndOfStream)
            {
                string employee = readerE.ReadLine();
                if (!string.IsNullOrEmpty(employee))
                {
                    string[] empoyeeData = employee.Split(',');
                    string productName = empoyeeData[0];
                    string email = empoyeeData[1];
                    long mobile = long.Parse(empoyeeData[2]);
                    string addres = empoyeeData[3];
                    string password = empoyeeData[4];
                    bool adminR = empoyeeData[5].ToLower().Equals("true");

                    Employee newEmployee = new Employee(productName, email, mobile, addres, password, adminR);

                    shopsEmployees.Add(newEmployee);
                }

            }
            fileE.Close();
            readerE.Close();
            Console.WriteLine("Predefined Employees registered successfully!");
        }
        public void WriteEmployeeTXT(string employee)
        {
            StreamWriter writerE = File.AppendText(filePathE);
            Console.WriteLine(employee);
            writerE.WriteLine(employee);
            writerE.Close();
            
        }
        public void HireEmployee(Employee theEmployee)
        {
            Console.WriteLine(" -> Hire employee for the shop <-");
            Console.WriteLine("Insert the data of the new employee on one line:");
            Console.WriteLine("(productName,email,mobile,address,password,adminRights)");
            Console.WriteLine("example:");
            Console.WriteLine("Pedro,pedro@email.com,9876543210,street 10 number 10,pedro,true");
            //try
            //{
                Employee hiredEmployee = validInput.ValidatingNewEmployee();
            //}
            //catch
            //{
                
            //}
            
            
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
                    WriteEmployeeTXT(hiredEmployee.GetEmployeeString());
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
        public void PrintRegisteredEmployees()
        {
            Console.WriteLine("ID \tName");
            for (int i = 0; i < ShopsEmployees.Count; i++)
            {
                Console.WriteLine(i + "\t" + ShopsEmployees[i].Name);
            }
        }


        /*Costumer Methods*/
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
        public void CostumerMenu(Costumer theCostumer)
        {
            int answerE;

            do
            {
                Console.Clear();
                Console.WriteLine(" -> Costumer MENU for " + theCostumer.Name + "<-");
                Console.WriteLine("1.- Print products registered on the shop.");
                Console.WriteLine("2.- Purchease a product.");
                Console.WriteLine("3.- Check current order.");
                Console.WriteLine("4.- Checkout order.");
                Console.WriteLine("5.- Edit costumer data.");
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
