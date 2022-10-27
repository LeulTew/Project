using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }
        string itID = "";
        string custID = "";
        string custN = "";
        static int orders = 0;
        double tot=0;
        static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString);
        
        static SqlTransaction transaction;

        private void Order_Load(object sender, EventArgs e)
        {
            conn.Open();
            transaction = conn.BeginTransaction();
            string username = "";
            foreach (var item in Transfer.getuser())
            {
                username = item.usern;
            }
            string qur = "SELECT FName,LName,Id FROM Customer WHERE Username = '" + username + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label13.Text = dr.GetValue(0).ToString()+" "+ dr.GetValue(1).ToString();
                custID = dr.GetValue(2).ToString();
            }
            custN = label13.Text;
            DBcon.con().Close();
            string cb = "Select ItemName from Item";
            SqlCommand cd = new SqlCommand(cb, DBcon.con());
            SqlDataAdapter sdr = new SqlDataAdapter(cd);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            comboBoxItemN.DataSource = dt;
            comboBoxItemN.DisplayMember = "ItemName";
            DBcon.con().Close();
        }
        
        /*
        public void getitemid()
        {
            SqlConnection can = new SqlConnection(@"Data Source = EMPEROR\SQLEXPRESS;Initial Catalog =Empinfo;Integrated Security =true");
            string qur = "SELECT ID FROM Item WHERE ItemName = '" + comboBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(qur, can);
            can.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                itID= dr.GetValue(0).ToString();
            }
            can.Close();
        }
        */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qur = "SELECT Price,ID FROM Item WHERE ItemName = '" + comboBoxItemN.Text + "'";       
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label6.Text = dr.GetValue(0).ToString();
                itID = dr.GetValue(0).ToString();
            }
            DBcon.con().Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //Name
            if (string.IsNullOrEmpty(comboBoxItemN.Text))
            {
                isVal = false;
                errorProviderName.SetError(comboBoxItemN, "Name is Required");
            }
            else
                errorProviderName.Clear();
            //Quantity
            if (string.IsNullOrEmpty(textBoxQty.Text))
            {
                isVal = false;
                errorProviderQty.SetError(textBoxQty, "Quantity is Required");
            }
            else
                errorProviderQty.Clear();
            try
            {
                errorProviderQty.Clear();
                int.Parse(textBoxQty.Text);
            }
            catch
            {
                errorProviderQty.SetError(textBoxQty, "Quantity is required");
            }

            //ADD Cart
            if (isVal)
            {
                double pri = double.Parse(label6.Text);
                orders++;
                //MessageBox.Show(pri.ToString());
                tot += pri * int.Parse(textBoxQty.Text);
                string username = "";
                foreach (var item in Transfer.getuser())
                {
                    username = item.usern;
                }
                try
                {
                    string query = "[Order Item] @qty, @itemName, @userName, DEFAULT";
                    SqlCommand cmdd = new SqlCommand(query, conn, transaction);
                    cmdd.Parameters.Add("@qty", SqlDbType.Int).Value = int.Parse(textBoxQty.Text);
                    cmdd.Parameters.Add("@itemName", SqlDbType.VarChar).Value = comboBoxItemN.Text;
                    cmdd.Parameters.Add("@userName", SqlDbType.VarChar).Value = username;
                    //cmdd.Parameters.Add("@CompanyN", SqlDbType.VarChar).Value = "";
                    int rowAff = cmdd.ExecuteNonQuery();
                    if (rowAff > 0)
                        MessageBox.Show("Item Added");
                    else
                        MessageBox.Show("Item Not Added");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }



                //string qur = "Select * from ItemOrder where CustID ='"+custID+"'";
                string qur = "EXEC [Display User Order] '" + username + "', '" + orders + "'";
                SqlCommand cmd = new SqlCommand(qur, conn, transaction);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                //sd.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
                //DBcon.con().Close();
                label10.Text = tot.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label9.Text = custN;
            label10.Text = tot.ToString();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            transaction.Rollback();
            dataGridView1.DataSource=null;
            conn.Close();
            conn.Open();
            transaction = conn.BeginTransaction();
            orders = 0;
            tot = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (orders > 0)
            {

                transaction.Commit();
                dataGridView1.DataSource = null;
                if (checkBox1.Checked)
                    MessageBox.Show("Purchased! \n Awaiting Delivery");
                else
                    MessageBox.Show("Purchased!");
                conn.Close();
                conn.Open();
                transaction = conn.BeginTransaction();
                orders = 0;
                tot = 0;
            }
            else
                errorProviderBuy.SetError(button2, "Cart Empty");
        }
    }
}
