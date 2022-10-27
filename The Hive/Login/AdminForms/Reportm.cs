using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.AdminForms
{
    public partial class Reportm : Form
    {
        public Reportm()
        {
            InitializeComponent();
        }

        private void Reportm_Load(object sender, EventArgs e)
        {
            // All Transactions
            string qur = "[Display All Order]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView2.DataSource = dt;
            DBcon.con().Close();

            //Items Purchased
            string qur2 = "[Purchased Items]";
            SqlCommand cmd2 = new SqlCommand(qur2, DBcon.con());
            SqlDataAdapter sd2 = new SqlDataAdapter(cmd2);
            sd2.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt2 = new DataTable();
            sd2.Fill(dt2);
            dataGridView3.DataSource = dt2;
            DBcon.con().Close();

            //CHANGES LOG
            
            string qur3 = "[Display Changes] '" + dateTimePicker1.Value.ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(qur3, DBcon.con());
            SqlDataAdapter sd3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            sd3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            DBcon.con().Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //User
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                isVal = false;
                epUser.SetError(textBox1, "UserName Required");
            }
            else
            {
                epUser.Clear();
                try
                {

                    string qur = "SELECT * FROM dbo.Customer WHERE Username = '" + textBox1.Text + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        isVal = false;
                        epUser.SetError(textBox1, "Enter valid userName");
                    }
                    else
                        epUser.Clear();
                }
                catch
                {
                    isVal = false;
                    epUser.SetError(textBox1, "UserName Required");
                };
            }
            if (isVal)
            {
                string qur = "[View Customer Transaction] '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                //sd.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView4.DataSource = dt;
                DBcon.con().Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //CHANGES LOG
            string qur3 = "[Display Changes] '" + dateTimePicker1.Value.ToString() + "'";
            SqlCommand cmd3 = new SqlCommand(qur3, DBcon.con());
            SqlDataAdapter sd3 = new SqlDataAdapter(cmd3);
            DataTable dt3 = new DataTable();
            sd3.Fill(dt3);
            dataGridView1.DataSource = dt3;
            DBcon.con().Close();
        }

        private void btnRoll_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null)
            {
                string qur = "[UNDO Change] @date";
                SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = dateTimePicker1.Value;
                var rowAff = cmd.ExecuteNonQuery();
                if (rowAff != 0)
                    MessageBox.Show("Change reverted");
                else
                    MessageBox.Show("Failed");
                dateTimePicker1_ValueChanged(sender, e);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //ALL Trans
            string qur = "[Display All Order]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView2.DataSource = dt;
            DBcon.con().Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Items Purchased
            string qur2 = "[Purchased Items]";
            SqlCommand cmd2 = new SqlCommand(qur2, DBcon.con());
            SqlDataAdapter sd2 = new SqlDataAdapter(cmd2);
            sd2.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt2 = new DataTable();
            sd2.Fill(dt2);
            dataGridView3.DataSource = dt2;
            DBcon.con().Close();
        }
    }
}
