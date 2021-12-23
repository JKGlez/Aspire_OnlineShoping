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
    public class EmployeeDataAccess
    {

        public void ReadEmployeesDB(List<Employee> ShopsEmployees)
        {
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                string nameE, emailE, addressE, passwordE;
                long mobileE;
                bool adminRE;


                SqlCommand cmd = new SqlCommand("sp_readAllEmployees", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader readerProducts = cmd.ExecuteReader();
                DataTable dtt = new DataTable();
                dtt.Load(readerProducts);
                
                foreach (DataRow item in dtt.Rows)
                {
                    nameE = item["NAME_EMPLOYEE"].ToString();
                    emailE = item["EMAIL_EMPLOYEE"].ToString();
                    mobileE = long.Parse(item["MOBILE_EMPLOYEE"].ToString());
                    addressE = item["ADDRESS_EMPLOYEE"].ToString();
                    passwordE = item["PASSWORD_EMPLOYEE"].ToString();
                    adminRE = bool.Parse(item["ADMIN_RIGHTS_EMPLOYEE"].ToString());
                    Employee employeeDB = new Employee(nameE,emailE,mobileE,addressE,passwordE,adminRE);
                    ShopsEmployees.Add(employeeDB);
                }
                
            }
        }

        public int WriteEmployeeDB(Employee newEmployee)
        {
            int res;
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_insertNewEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@nameE", SqlDbType.NVarChar).Value = newEmployee.Name;
                cmd.Parameters.AddWithValue("@emailE", SqlDbType.NVarChar).Value = newEmployee.Email;
                cmd.Parameters.AddWithValue("@mobileE", SqlDbType.NVarChar).Value = newEmployee.Mobile.ToString();
                cmd.Parameters.AddWithValue("@addressE", SqlDbType.NVarChar).Value = newEmployee.Addres;
                cmd.Parameters.AddWithValue("@passwordE", SqlDbType.NVarChar).Value = newEmployee.Password;
                cmd.Parameters.AddWithValue("@adminRE", SqlDbType.Bit).Value = newEmployee.AdminRights;
                res = cmd.ExecuteNonQuery();
                return res;
            }
        }
        
        public void DeleteEmployeeDB(string nameDelete)
        {
            int res = - 1;
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_deleteEmployeeByName", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nameDelete", SqlDbType.NVarChar).Value = nameDelete;
                res = cmd.ExecuteNonQuery();
            }
        }
        /*
        public int UpdateProductDB(Product productToModify, Product newProduct, int stockToAdd)
        {
            int res = -1;
            using (SqlConnection conn = DbCommonConection.GetConnection())
            {
                //Console.WriteLine(product.GetType().ToString());
                if (productToModify.GetType().ToString().Equals("OnlineShopV3.BO.Grocery"))
                {
                    Grocery productToAdd = (Grocery)newProduct;
                    SqlCommand cmd = new SqlCommand("sp_updateGrocerieByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
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
                    SqlCommand cmd = new SqlCommand("sp_updateElectronicByID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
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
        */
    }
}
