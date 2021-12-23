using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OnlineShopV3.BO;
using OnlineShopV3.DL;

namespace OnlineShopV3.SL
{
    public class ProductController
    {
        InputValidation validInput = new InputValidation();
        ProductDataAccess productDL = new ProductDataAccess();

        public Product CreateProduct()
        {
            Product currentProduct = null;
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
                    Console.WriteLine("\t -> REGISTERING A GROCERY <-");
                    Console.WriteLine("Enter the datails of the product on one line:");
                    currentProduct = CreateGrocery();
                    break;

                case 2:
                    Console.Clear();
                    Console.WriteLine("\t -> REGISTERING AN ELECTRONIC <-");
                    Console.WriteLine("Enter the datails of the product on one line:");
                    currentProduct = CreateElectronic();
                    break;
            }
            //fileP.Close();
            //writerP.Close();
            return currentProduct;
        }
        public Grocery CreateGrocery()
        {
            string product;
            string[] productData;
            string productName;
            Grocery currentProduct = null;

            DateTime productManufacturingDate;
            DateTime productExpiringDate;
         

            Console.WriteLine("(ID,ProductName,Price,ExpireDate,ManufDate)");
            Console.WriteLine("Example:");
            Console.WriteLine("(123,Eggs,5,2021-11-30,2021-11-20)");
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
            //ShopsProducts.Add(currentProduct);
            Console.WriteLine("PRODUCT SUMARY");
            //writerP.WriteLine(productId + ",Groceries," + productName + "," + productPrice + "," + stock + "," + productExpiringDate.ToString("yyyy/MM/dd") + "," + productManufacturingDate.ToString("yyyy/MM/dd"));
            Console.WriteLine(currentProduct.PrintProduct());
            
            return currentProduct;
        }
        public Electronic CreateElectronic()
        {
            string product;
            string[] productData;
            string productName;
            Electronic currentProduct = null;



            Console.WriteLine("(ID,ProductName,Price,Guarantee)");
            Console.WriteLine("Example:");
            Console.WriteLine("(123,Fridge,2500,2022-12-12)");
            product = Console.ReadLine();
            productData = product.Split(',');
            int.TryParse(productData[0], out int productId);
            productName = productData[1];
            double.TryParse(productData[2], out double productPrice);
            string[] YMD = productData[3].Split('/');
            DateTime productGuarantee = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
            currentProduct = new Electronic(productId, "Electronic", productName, productPrice, productGuarantee);

            Console.WriteLine("PRODUCT SUMARY");
            //writerP.WriteLine(productId + ",Electronics," + productName + "," + productPrice + "," + stock + "," + productGuarantee.ToString("yyyy/MM/dd"));

            //Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
            Console.WriteLine(currentProduct.PrintProduct());

            return currentProduct;
        }
        public int RegisterProduct(Product newProduct, int newStock)
        {
            Console.Clear();
            Console.WriteLine("\t -> PRODUCT REGISTERED <-");
            return productDL.WriteProductDB(newProduct, newStock);
            
        }
        public void UpdateProductInfo(Employee theEmployee, List<Product> ShopsProducts, List<int> ShopsProductsStock)
        {

            Console.WriteLine("Select the ID of the product to discotinue:");

            Product selectedProduct = validInput.ValidatingProdcutByID(ShopsProducts);

            Console.WriteLine("The product to Modify is:");
            Console.WriteLine(selectedProduct);
            Console.WriteLine("continue?: (yes/no)");
            if (validInput.ValidatingYesNo().Equals("yes"))
            {
                Console.WriteLine("->  Insert the information of the new product  <-");
                Product newProduct = CreateProduct();
                Console.WriteLine("Insert the stock for that product:.");
                int stock = int.Parse(Console.ReadLine());
                while (stock < 1)
                {
                    Console.WriteLine("ERROR, Select a valid stock number:");
                    stock = int.Parse(Console.ReadLine());
                }
                Console.WriteLine("Insert admin password (" + theEmployee.Name + "): ");
                if (validInput.ValidatingPassword(theEmployee))
                {
                   
                    int index = ShopsProducts.IndexOf(selectedProduct);
                    ShopsProducts[index] = newProduct;
                    //ShopsProducts.Remove(selectedProduct);
                    ShopsProductsStock[index] = stock;
                    //ShopsProductsStock.RemoveAt(index);

                    productDL.UpdateProductDB(selectedProduct, newProduct, stock);
                    

                      
                    Console.WriteLine("Password accepted");
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
        public void DiscontinueProduct(Employee theEmployee, List<Product> ShopsProducts, List<int> ShopsProductsStock)
        {

            Console.WriteLine("Select the ID of the product to discotinue:");

            Product selectedProduct = validInput.ValidatingProdcutByID(ShopsProducts);

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
                    productDL.DeleteProductsDB(selectedProduct.Id);
                    //XmlDocument xmlProductsDoc = new XmlDocument();
                    //xmlProductsDoc.Load("./productsXML.xml");
                    //XmlNode rootProducts = xmlProductsDoc.DocumentElement;
                    //XmlNodeList productList = xmlProductsDoc.SelectNodes("ProductsOnTheShop/Product");
                    //foreach (XmlNode item in productList)
                    //{
                    //    if (item.SelectSingleNode("ID").InnerText.Equals(selectedProduct.Id.ToString()))
                    //    {
                    //        XmlNode productRemove = item;
                    //        rootProducts.RemoveChild(productRemove);
                    //        xmlProductsDoc.Save("./productsXML.xml");
                    //    }
                    //}
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

    }
}
