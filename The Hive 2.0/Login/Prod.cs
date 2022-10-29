using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Login
{
    internal class Prod
    {
        public static List<Prod> prod = new List<Prod>();
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string imageLoc { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool Enabled { get; set; }
        public int CategortyID { get; set; }
        public string CategortyName { get; set; }

        public int save()
        {
            try
            {
                string query = "[Insert Item] @itemN , @price ,@itemPic , @Desc , @qty , @exp , @catN";
                SqlCommand cmd = new SqlCommand(query, DBcon.con());
                cmd.Parameters.Add("@itemN", SqlDbType.VarChar).Value = this.Name;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = this.Price;
                cmd.Parameters.Add("@itemPic", SqlDbType.VarChar).Value = this.imageLoc;
                cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = this.Description;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = this.Quantity;
                cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = this.ExpireDate;
                cmd.Parameters.Add("@catN", SqlDbType.VarChar).Value = this.CategortyName;
                int rowAff = cmd.ExecuteNonQuery();
                DBcon.con().Close();
                prod.Add(this);
                return rowAff;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }
            /*
            private static List<prop> prod = new List<prop>();
            public int id { get; set; }
            public string fname { get; set; }
            public string lname { get; set; }
            public string email { get; set; }
            public DateTime date { get; set; }
            public string gender { get; set; }
            public string usern { get; set; }
            public string passw { get; set; }
            public string iname { get; set; }
            public string idesc { get; set; }
            public DateTime expdate { get; set; }
            public int stock { get; set; }
            public bool isAvail { get; set; }
            public int catagoryid { get; set; }
            public string picloc { get; set; }
            public double money { get; set; }*/

            
        /*
        public void itemsave()
        {
            try
            {
                
                //string query = "INSERT INTO Item values(@image,@iname,@price,@expdate,@idescr,@qunt,@catid,@isavail)";
                string query = "Execute [Insert Item] @image,@iname,@price,@expdate,@idescr,@qunt,@catid,@isavail";
                SqlCommand cmd = new SqlCommand(query, Conn());
                 cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@image", SqlDbType.VarChar).Value = this.picloc;
                cmd.Parameters.Add("@iname", SqlDbType.VarChar).Value = this.iname;
                cmd.Parameters.Add("@catid", SqlDbType.Int).Value = this.catagoryid;
                cmd.Parameters.Add("@expdate", SqlDbType.DateTime).Value = this.expdate;
                cmd.Parameters.Add("@idescr", SqlDbType.VarChar).Value = this.idesc;
                cmd.Parameters.Add("@qunt", SqlDbType.Int).Value = this.stock;
                cmd.Parameters.Add("@price", SqlDbType.Float).Value = this.money;
                cmd.Parameters.Add("@isavail", SqlDbType.Bit).Value = this.isAvail;
                int rowaff = cmd.ExecuteNonQuery();
                Conn().Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            prod.Add(this);

        }
        */
        public static List<Prod> getOther()
        {
            string query = "SELECT ItemPic,ItemName,Price FROM Item I JOIN ItemCategory C ON I.CategoryID = C.Id WHERE C.CategoryName = 'Other'";
            try
            {
                prod.Clear();
                SqlCommand cmd = new SqlCommand(query, Conn());
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Prod p = new Prod();
                    p.imageLoc = result[0].ToString();
                    p.Name = result[1].ToString();
                    p.Price = double.Parse(result[2].ToString());
                    prod.Add(p);
                }
                Conn().Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return prod;
        }
        public static List<Prod> getitem()
        {
            string query = "SELECT ItemPic,ItemName,Price FROM Item";
            try
            {
                prod.Clear();
                SqlCommand cmd = new SqlCommand(query, Conn());
                var result = cmd.ExecuteReader();
                while (result.Read())
                {
                    Prod p = new Prod();
                    p.imageLoc = result[0].ToString();
                    p.Name = result[1].ToString();                  
                    p.Price = double.Parse(result[2].ToString());
                    prod.Add(p);
                }
                Conn().Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return prod;
        }
        public static SqlConnection Conn()
        {
            return DBcon.con();
        }

        public static Prod findItem(string iName)
        {
            string name = iName;
            string qur = "[Search Item By Name] '" + name + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                Prod item = new Prod();
                while (dr.Read())
                {
                    item.Name = iName;
                    item.Id = (int)dr[0];
                    item.Price = (double)dr[2];
                    item.imageLoc = dr.GetValue(3).ToString();
                    item.Description = dr.GetValue(4).ToString();
                    item.ExpireDate = (DateTime)dr[6];
                    item.Quantity = (int)dr[5];
                    item.Enabled = (bool)dr[7];
                    item.CategortyID = (int)dr[8];
                }
                DBcon.con().Close();
                return item;
            }
            else
                return null;
        }

        public int update()
        {
            try
            {
                string query = "Execute [Update Item] @id, @itemN, @price, @itemPic, @Desc, @qty, @exp, @catN";
                SqlCommand cmd = new SqlCommand(query, DBcon.con());
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.Id;
                cmd.Parameters.Add("@itemN", SqlDbType.VarChar).Value = this.Name;
                cmd.Parameters.Add("@price", SqlDbType.Money).Value = this.Price;
                cmd.Parameters.Add("@itemPic", SqlDbType.VarChar).Value = this.imageLoc;
                cmd.Parameters.Add("@Desc", SqlDbType.VarChar).Value = this.Description;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = this.Quantity;
                cmd.Parameters.Add("@exp", SqlDbType.DateTime).Value = this.ExpireDate;
                cmd.Parameters.Add("@catN", SqlDbType.VarChar).Value = this.CategortyName;

                int rowAff = cmd.ExecuteNonQuery();
                DBcon.con().Close();
                prod.Add(this);
                return rowAff;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }

    }
}
