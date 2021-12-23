using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public void ReadProductsDBRudeWay(List<Product> ShopsProducts, List<int> ShopsProductsStock)
        {
            try
            {
                using (SqlConnection conn = DbCommonConection.GetConnection())
                {
                        string nameP,categoryP,expireDP,manufDP,guaranteeDP;
                        int idP, stockP;
                        double priceP;
                        Grocery groceryDB;
                        Electronic electronicDB;

                        string sqlQueryString = "SELECT * FROM tbl_products";
                        SqlDataAdapter sda = new SqlDataAdapter(sqlQueryString,conn);
                        DataSet dts = new DataSet();
                        sda.Fill(dts, "tbl_products");
                        DataTable dtt = new DataTable();
                        dtt = dts.Tables["tbl_products"];


                    foreach (DataRow item in dtt.Rows)
                    {
                        categoryP = item["CATEGORY_PRODUCT"].ToString();
                        nameP = item["NAME_PRODUCT"].ToString();
                        idP = int.Parse(item["ID_PRODUCT"].ToString());
                        priceP = double.Parse(item["PRICE_PRODUCT"].ToString());
                        stockP = int.Parse(item["STOCK_PRODUCT"].ToString());

                        

                        if (categoryP.ToLower().Equals("groceries"))
                        {
                            expireDP = item["EXPIRE_DATE_PRODUCT"].ToString().Substring(0, 10);
                            string[] expireS = expireDP.Split('/');
                            DateTime expire = new DateTime(int.Parse(expireS[2]), int.Parse(expireS[1]), int.Parse(expireS[0]));

                            manufDP = item["MANUF_DATE_PRODUCT"].ToString();
                            string[] manufS = expireDP.Split('/');
                            DateTime manuf = new DateTime(int.Parse(manufS[2]), int.Parse(manufS[1]), int.Parse(manufS[0]));

                            groceryDB = new Grocery(idP, categoryP, nameP, priceP, expire, manuf);
                            ShopsProducts.Add(groceryDB);
                            ShopsProductsStock.Add(stockP);
                        }
                        else if (categoryP.ToLower().Equals("electronics"))
                        {
                            guaranteeDP = item["GUARANTEE_DATE_PRODUCT"].ToString().Substring(0, 10);
                            string[] guaranteeS = guaranteeDP.Split('/');
                            DateTime guarantee = new DateTime(int.Parse(guaranteeS[2]), int.Parse(guaranteeS[1]), int.Parse(guaranteeS[0]));
                            electronicDB = new Electronic(idP, categoryP, nameP, priceP, guarantee);
                            ShopsProducts.Add(electronicDB);
                            ShopsProductsStock.Add(stockP);
                        }
                        else
                        {
                           
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
        public int WriteProductDBRudeWay(Product product, int stockToAdd)
        {
            
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                //Console.WriteLine(product.GetType().ToString());
                if (product.GetType().ToString().Equals("OnlineShopV3.BO.Grocery")) {
                    Grocery productToAdd = (Grocery)product;
                    string  sqlQueryString = string.Format("INSERT INTO tbl_products (CATEGORY_PRODUCT,ID_PRODUCT,NAME_PRODUCT,PRICE_PRODUCT,STOCK_PRODUCT,EXPIRE_DATE_PRODUCT,MANUF_DATE_PRODUCT) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", 
                                                   productToAdd.Category, productToAdd.Id, productToAdd.Name,productToAdd.Price,stockToAdd,productToAdd.ExpireDate.ToString("yyyy/MM/dd"), productToAdd.ManufDate.ToString("yyyy/MM/dd"));
                    SqlCommand Command = new SqlCommand(sqlQueryString,conn);
                    return Command.ExecuteNonQuery();
                }
                else if (product.GetType().ToString().Equals("OnlineShopV3.BO.Electronic"))
                {
                    Electronic productToAdd = (Electronic)product;
                    string sqlQueryString = string.Format("INSERT INTO tbl_products (CATEGORY_PRODUCT,ID_PRODUCT,NAME_PRODUCT,PRICE_PRODUCT,STOCK_PRODUCT,GUARANTEE_DATE_PRODUCT) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                   productToAdd.Category, productToAdd.Id, productToAdd.Name, productToAdd.Price, stockToAdd, productToAdd.Guarantee.ToString("yyyy/MM/dd"));
                    SqlCommand Command = new SqlCommand(sqlQueryString, conn);
                    return Command.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }
        }
        public void ReadProductsDB(List<Product> ShopsProducts, List<int> ShopsProductsStock)
        {
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                string nameP, categoryP, expireDP, manufDP, guaranteeDP;
                int idP, stockP;
                double priceP;
                Grocery groceryDB;
                Electronic electronicDB;

                SqlCommand cmd = new SqlCommand("sp_readAllProducts", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader readerProducts = cmd.ExecuteReader();
                DataTable dtt = new DataTable();
                dtt.Load(readerProducts);

                foreach (DataRow item in dtt.Rows)
                {
                    categoryP = item["CATEGORY_PRODUCT"].ToString();
                    nameP = item["NAME_PRODUCT"].ToString();
                    idP = int.Parse(item["ID_PRODUCT"].ToString());
                    priceP = double.Parse(item["PRICE_PRODUCT"].ToString());
                    stockP = int.Parse(item["STOCK_PRODUCT"].ToString());

                    if (categoryP.ToLower().Equals("groceries"))
                    {
                        expireDP = item["EXPIRE_DATE_PRODUCT"].ToString().Substring(0, 10);
                        string[] expireS = expireDP.Split('/');
                        DateTime expire = new DateTime(int.Parse(expireS[2]), int.Parse(expireS[1]), int.Parse(expireS[0]));

                        manufDP = item["MANUF_DATE_PRODUCT"].ToString();
                        string[] manufS = expireDP.Split('/');
                        DateTime manuf = new DateTime(int.Parse(manufS[2]), int.Parse(manufS[1]), int.Parse(manufS[0]));

                        groceryDB = new Grocery(idP, categoryP, nameP, priceP, expire, manuf);
                        ShopsProducts.Add(groceryDB);
                        ShopsProductsStock.Add(stockP);
                    }
                    else if (categoryP.ToLower().Equals("electronics"))
                    {
                        guaranteeDP = item["GUARANTEE_DATE_PRODUCT"].ToString().Substring(0, 10);
                        string[] guaranteeS = guaranteeDP.Split('/');
                        DateTime guarantee = new DateTime(int.Parse(guaranteeS[2]), int.Parse(guaranteeS[1]), int.Parse(guaranteeS[0]));
                        electronicDB = new Electronic(idP, categoryP, nameP, priceP, guarantee);
                        ShopsProducts.Add(electronicDB);
                        ShopsProductsStock.Add(stockP);
                    }
                    else
                    {

                    }
                }
            }
        }
        public int WriteProductDB(Product product, int stockToAdd)
        {
            int res = -1;
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                //Console.WriteLine(product.GetType().ToString());
                if (product.GetType().ToString().Equals("OnlineShopV3.BO.Grocery"))
                {
                    Grocery productToAdd = (Grocery)product;
                    SqlCommand cmd = new SqlCommand("sp_insertNewGrocesy", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@categoryP", SqlDbType.NVarChar).Value = "groceries";
                    cmd.Parameters.AddWithValue("@idP", SqlDbType.Int).Value = productToAdd.Id;
                    cmd.Parameters.AddWithValue("@nameP", SqlDbType.NVarChar).Value = productToAdd.Name;
                    cmd.Parameters.AddWithValue("@priceP", SqlDbType.Decimal).Value = productToAdd.Price;
                    cmd.Parameters.AddWithValue("@stockP", SqlDbType.Int).Value = stockToAdd;
                    cmd.Parameters.AddWithValue("@expireP", SqlDbType.Date).Value = productToAdd.ExpireDate.ToString("yyyy/MM/dd");
                    cmd.Parameters.AddWithValue("@manufP", SqlDbType.Date).Value = productToAdd.ManufDate.ToString("yyyy/MM/dd");
                    res = cmd.ExecuteNonQuery();
                    
                }
                else if (product.GetType().ToString().Equals("OnlineShopV3.BO.Electronic"))
                {
                    Electronic productToAdd = (Electronic)product;
                    SqlCommand cmd = new SqlCommand("sp_insertNewElectronic", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@categoryP", SqlDbType.NVarChar).Value = "electronics";
                    cmd.Parameters.AddWithValue("@idP", SqlDbType.Int).Value = productToAdd.Id;
                    cmd.Parameters.AddWithValue("@nameP", SqlDbType.NVarChar).Value = productToAdd.Name;
                    cmd.Parameters.AddWithValue("@priceP", SqlDbType.Decimal).Value = productToAdd.Price;
                    cmd.Parameters.AddWithValue("@stockP", SqlDbType.Int).Value = stockToAdd;
                    cmd.Parameters.AddWithValue("@guaranteeP", SqlDbType.Date).Value = productToAdd.Guarantee.ToString("yyyy/MM/dd");
                    res = cmd.ExecuteNonQuery();
                    
                }
                else
                {
                    res = -1;
                }
                return res;
            }
        }
        public void DeleteProductsDB(int idDelete)
        {
            int res = - 1;
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_deleteProductById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idDelete", SqlDbType.Int).Value = idDelete;
                res = cmd.ExecuteNonQuery();
            }
        }
        public int UpdateProductDB(Product productToModify, Product newProduct, int stockToAdd)
        {
            int res = -1;
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                //Console.WriteLine(product.GetType().ToString());
                if (productToModify.GetType().ToString().Equals("OnlineShopV3.BO.Grocery"))
                {
                    Grocery productToAdd = (Grocery)newProduct;
                    SqlCommand cmd = new SqlCommand("sp_updateGrocerieByID", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@idP", SqlDbType.Int).Value = productToAdd.Id;
                    cmd.Parameters.AddWithValue("@nameP", SqlDbType.NVarChar).Value = productToAdd.Name;
                    cmd.Parameters.AddWithValue("@priceP", SqlDbType.Decimal).Value = productToAdd.Price;
                    cmd.Parameters.AddWithValue("@stockP", SqlDbType.Int).Value = stockToAdd;
                    cmd.Parameters.AddWithValue("@expireP", SqlDbType.Date).Value = productToAdd.ExpireDate.ToString("yyyy/MM/dd");
                    cmd.Parameters.AddWithValue("@manufP", SqlDbType.Date).Value = productToAdd.ManufDate.ToString("yyyy/MM/dd");
                    res = cmd.ExecuteNonQuery();

                }
                else if (productToModify.GetType().ToString().Equals("OnlineShopV3.BO.Electronic"))
                {
                    Electronic productToAdd = (Electronic)newProduct;
                    SqlCommand cmd = new SqlCommand("sp_updateElectronicByID", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@idP", SqlDbType.Int).Value = productToAdd.Id;
                    cmd.Parameters.AddWithValue("@nameP", SqlDbType.NVarChar).Value = productToAdd.Name;
                    cmd.Parameters.AddWithValue("@priceP", SqlDbType.Decimal).Value = productToAdd.Price;
                    cmd.Parameters.AddWithValue("@stockP", SqlDbType.Int).Value = stockToAdd;
                    cmd.Parameters.AddWithValue("@guaranteeP", SqlDbType.Date).Value = productToAdd.Guarantee.ToString("yyyy/MM/dd");
                    res = cmd.ExecuteNonQuery();
                }
                else
                {
                    res = -1;
                }
                return res;
            }
        }
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
                if (theProduct.GetType().ToString().Equals("OnlineShopV3.BO.Grocery"))
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
                else if (theProduct.GetType().ToString().Equals("OnlineShopV3.BO..Electronic"))
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
