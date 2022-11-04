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
    internal class User
    {
        public static List<User> users = new List<User>();
        public string ID { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string LName { get; set; }
        public string FName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public int save()
        {
            try
            {
                //DBcon c = new DBcon();
                string query = "Execute [Insert User] @Username, @Name, @passw, @Email, @dob, @gender, DEFAULT";
                string FullName = this.FName + " " + this.LName;
                SqlCommand cmd = new SqlCommand(query, DBcon.con());
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = this.UserName;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = FullName;
                cmd.Parameters.Add("@passw", SqlDbType.VarChar).Value = this.Password;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = this.Email;
                cmd.Parameters.Add("@dob", SqlDbType.DateTime).Value = this.Dob;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = this.Gender;


                int rowAff = cmd.ExecuteNonQuery();
                DBcon.con().Close();
                users.Add(this);
                return rowAff;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }

        }

        public int update(string rid)
        {
            try
            {
                string query = "Execute [Update User] @id, @uN, @pass, @fN, @lN, @dob, @email, @gender";
                SqlCommand cmd = new SqlCommand(query, DBcon.con());
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = this.ID;
                cmd.Parameters.Add("@uN", SqlDbType.VarChar).Value = this.UserName;
                cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = this.Password;
                cmd.Parameters.Add("@fN", SqlDbType.VarChar).Value = this.FName;
                cmd.Parameters.Add("@lN", SqlDbType.VarChar).Value = this.LName;
                cmd.Parameters.Add("@dob", SqlDbType.DateTime).Value = this.Dob;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = this.Email;
                cmd.Parameters.Add("@gender", SqlDbType.VarChar).Value = this.Gender;

                int rowAff = cmd.ExecuteNonQuery();
                DBcon.con().Close();
                users.Add(this);
                return rowAff;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }

        public static List<User> getAll()
        {
            string query = "EXECUTE [Display All User]";
            //DBcon c = new DBcon();
            try
            {
                users.Clear();
                SqlCommand cmd = new SqlCommand(query, DBcon.con());
                SqlDataReader result = cmd.ExecuteReader();
                while (result.Read())
                {
                    User u = new User();
                    u.ID = (string)result["Id"];
                    u.UserName = (string)result["Username"];
                    u.Password = (string)result["Pass"];
                    u.FName = (string)result["FName"].ToString();
                    u.LName = (string)result["LName"].ToString();
                    u.Dob = (DateTime)result["DOB"];
                    u.Email = (string)result["EmailAddress"];
                    u.Phone = (string)result["Phone"];
                    u.Gender = (string)result["Gender"];
                    users.Add(u);
                }
                /*SqlDataAdapter da = new SqlDataAdapter(query, c.con());
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataSet ds = new DataSet();
                da.Fill(ds, "User");
                foreach (DataRow row in ds.Tables["User"].Rows)
                {
                    User u = new User();
                    u.ID = (string)row[0];
                    u.UserName = (string)row[1];
                    u.Password = (string)row[2];
                    u.FName = row[3].ToString();
                    u.LName = row[4].ToString();
                    u.Dob = (DateTime)row[5];
                    u.Email = (string)row[6];
                    u.Phone = (string)row[7];
                    u.Gender = (string)row[8];
                    users.Add(u);
                }*/
                DBcon.con().Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return users;
        }
        public static User findUser(string uName)
        {
            string name = uName;
            string qur = "[Search User By Username] '" + name + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                User user = new User();
                while (dr.Read())
                {
                    user.UserName = dr.GetValue(1).ToString();
                    user.ID = dr.GetValue(0).ToString();
                    user.FName = dr.GetValue(3).ToString();
                    user.LName = dr.GetValue(4).ToString();
                    user.Email = dr.GetValue(6).ToString();
                    user.Dob = (DateTime)dr[5];
                    user.Gender = dr.GetValue(8).ToString();
                    user.Password = dr.GetValue(2).ToString();
                }
                DBcon.con().Close();
                return user;
            }
            else
                return null;
        }
    }
}
