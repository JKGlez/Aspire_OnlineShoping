using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using OnlineShopV3.BO;

namespace OnlineShopV3.DL
{
    public class DbCommonConection
    {
        public static SqlConnection GetConnection() {
            string connectionString = "Data Source = DESKTOP-4C0KIA6\\MSSQLSERVER_JK ; Initial Catalog = OnlineShop ; integrated security = true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
