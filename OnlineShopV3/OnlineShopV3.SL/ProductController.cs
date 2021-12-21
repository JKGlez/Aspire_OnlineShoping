using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OnlineShopV3.BO;

namespace OnlineShopV3.SL
{
    public class ProductController
    {
        public Product CreateProduct()
        {
            string product = Console.ReadLine();
            string[] productData = product.Split(',');
            int.TryParse(productData[0], out int productId);
            string productType = productData[1];
            string productName = productData[2];
            if (productType.ToLower().Equals("groceries"))
            {
                double.TryParse(productData[3], out double productPrice);
                int productStock = int.Parse(productData[4]);
                //ShopsProductsStock.Add(productStock);
                string[] YMD = productData[5].Split('/');
                DateTime productExpiringDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                YMD = productData[6].Split('/');
                DateTime productManufacturingDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                Product currentProduct = new Grocery(productId, productType, productName, productPrice, productExpiringDate, productManufacturingDate);
                //ShopsProducts.Add(currentProduct);
                return currentProduct;
            }
            else if (productType.ToLower().Equals("electronics"))
            {
                double.TryParse(productData[3], out double productPrice);
                int productStock = int.Parse(productData[4]);
                //ShopsProductsStock.Add(productStock);
                string[] YMD = productData[5].Split('/');
                DateTime productGuarantee = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                Product currentProduct = new Electronic(productId, productType, productName, productPrice, productGuarantee);
                //ShopsProducts.Add(currentProduct);
                return currentProduct;
            }
            return null;
        }

        public Product RegisterProduct()
        {
            string product;
            string[] productData;
            string productName;
            Product currentProduct = null;
            int stock = 0;


            string filePathP = @"C:\Users\JK\Desktop\Aspire_OnlineShop_V2.0\Files\Products.txt";
            StreamWriter writerP = File.AppendText(filePathP);

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
                    string[] YMD = productData[3].Split('/');
                    productExpiringDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                    YMD = productData[4].Split('/');
                    productManufacturingDate = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                    currentProduct = new Grocery(productId, "Groceries", productName, productPrice, productExpiringDate, productManufacturingDate);
                    //ShopsProducts.Add(currentProduct);
                    Console.WriteLine("PRODUCT TO ADD:");
                    writerP.WriteLine(productId + ",Groceries," + productName + "," + productPrice + "," + stock + "," + productExpiringDate.ToString("yyyy/MM/dd") + "," + productManufacturingDate.ToString("yyyy/MM/dd"));
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
                    YMD = productData[3].Split('/');
                    DateTime productGuarantee = new DateTime(int.Parse(YMD[0]), int.Parse(YMD[1]), int.Parse(YMD[2]));
                    currentProduct = new Electronic(productId, "Electronic", productName, productPrice, productGuarantee);

                    Console.WriteLine("PRODUCT TO ADD");
                    writerP.WriteLine(productId + ",Electronics," + productName + "," + productPrice + "," + stock + "," + productGuarantee.ToString("yyyy/MM/dd"));

                    Console.WriteLine("PRODUCT ADDED SUCCEFULLY");
                    Console.WriteLine(currentProduct.PrintProduct());
                    break;
            }
            //fileP.Close();
            writerP.Close();
            return currentProduct;
        }

    }
}
