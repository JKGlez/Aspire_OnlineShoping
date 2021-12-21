using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using OnlineShopV3.BO;

namespace OnlineShopV3.DL
{
    public class ProductDataAccess
    {
        public void ReadProductsXML(List<Product> ShopsProducts, List<int> ShopsProductsStock)
        {
            XmlDocument xmlDocProducts = new XmlDocument();

            try
            {
                xmlDocProducts.Load("./files/productsXML.xml");
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
                xmlDocProducts.Load("./files/productsXML.xml");
                //Product currentProduct = null;
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
                xmlDocProducts.Load("./files/productsXML.xml");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The XML with the data was not found...creating file");
                XmlNode rootNode = xmlDocProducts.CreateElement("ProductsOnTheShop");
                xmlDocProducts.AppendChild(rootNode);
                XmlDeclaration declaration = xmlDocProducts.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocProducts.InsertBefore(declaration, rootNode);
                xmlDocProducts.Save("/files/productsXML.xml");
            }
            catch (XmlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("The XML with the data is empty...creating format");
                XmlNode rootNode = xmlDocProducts.CreateElement("ProductsOnTheShop");
                xmlDocProducts.AppendChild(rootNode);
                XmlDeclaration declaration = xmlDocProducts.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDocProducts.InsertBefore(declaration, rootNode);
                xmlDocProducts.Save("/files/productsXML.xml");
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
                xmlDocProducts.Save("./files/productsXML.xml");
            }
        }
    }
}
