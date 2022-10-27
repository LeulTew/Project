using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace Login
{
    internal class DBcon
    {
        
        public static SqlConnection con()
        {
            SqlConnection con;
            //string path = @"Data Source = LAPTOP-NS88HFGR\SQLEXPRESS; Initial Catalog = HIVE; Integrated Security = true";
            string path = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            try
            {
                con = new SqlConnection(path);
                con.Open();
                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }


        }
    }
}
