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
    public partial class Company : Form
    {
        public Company()
        {
            InitializeComponent();
        }

        //LOAD
        private void Company_Load(object sender, EventArgs e)
        {
            string qurr = "Select Cname FROM Company";
            SqlCommand cmdd = new SqlCommand(qurr, DBcon.con());
            SqlDataAdapter sdr = new SqlDataAdapter(cmdd);
            DataTable dtt = new DataTable();
            sdr.Fill(dtt);
            comboBox1.DataSource = dtt;
            comboBox1.DisplayMember = "Cname";
            DBcon.con().Close();


            // TODO: This line of code loads data into the 'empinfoDataSet.Company' table. You can move, or remove it, as needed.
            string qur = "[Display Companies]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            DBcon.con().Close();
        }

        //ADD
        private void button1_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //Name
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                isVal = false;
                errorProviderName.SetError(textBox2, "Company Name Required");
            }
            else
            {
                errorProviderName.Clear();
                try
                {
                    string name = "";
                    string qur = "[Search Company By Name] '" + textBox2.Text + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while(dr.Read())
                            name = dr.GetValue(1).ToString();
                        if (name == textBox2.Text)
                        {
                            isVal = false;
                            errorProviderName.SetError(textBox2, "Company Name already Exists");
                        }
                        else
                            errorProviderName.Clear();
                    }
                }
                    
                catch
                {
                    isVal = false;
                    errorProviderName.SetError(textBox2, "Company Name Required");
                };
            }
            //Address
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                isVal = false;
                errorProviderAddress.SetError(textBox4, "Address is Required");
            }
            else
                errorProviderAddress.Clear();
            //Phone
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                isVal = false;
                errorProviderPhone.SetError(textBox3, "Phone is Required");
            }
            else
                errorProviderPhone.Clear();
            if (isVal)
            {
                try
                {
                    string query = "[Insert Company] @Name, @address, @phone";
                    SqlCommand cmd = new SqlCommand(query, DBcon.con());
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = textBox2.Text;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = textBox4.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = textBox3.Text;
                    int rowAff = cmd.ExecuteNonQuery();
                    if (rowAff > 0)
                        MessageBox.Show("Registered Successfully");
                    else
                        MessageBox.Show("Registration Failed");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
                finally
                {
                    DBcon.con().Close();
                }
            }
        }

        //UPDATE
        private void button3_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //Name
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                isVal = false;
                errorProviderName.SetError(comboBox1, "Company Name Required");
            }
            else
                errorProviderName.Clear();
            //Address
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                isVal = false;
                errorProviderAddress.SetError(textBox5, "Address is Required");
            }
            else
                errorProviderAddress.Clear();
            //Phone
            if (string.IsNullOrEmpty(textBox6.Text))
            {
                isVal = false;
                errorProviderPhone.SetError(textBox6, "Phone is Required");
            }
            else
                errorProviderPhone.Clear();
            //Update
            if (isVal)
            {
                try
                {
                    string query = "Execute [Update Company] @Cname, @address, @tel";
                    SqlCommand cmd = new SqlCommand(query, DBcon.con());
                    cmd.Parameters.Add("@Cname", SqlDbType.VarChar).Value = comboBox1.Text;
                    cmd.Parameters.Add("@address", SqlDbType.VarChar).Value = textBox5.Text;
                    cmd.Parameters.Add("@tel", SqlDbType.Int).Value = int.Parse(textBox6.Text);

                    int rowAff = cmd.ExecuteNonQuery();
                    if (rowAff > 0)
                        MessageBox.Show("UPDATED Successfully");
                    else
                        MessageBox.Show("Update Failed");
                }
                catch
                {
                    MessageBox.Show("Update Error");
                }
                finally
                {
                    DBcon.con().Close();
                }
            }
        }

        //SEARCH
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string src = comboBox1.Text;
            string qur = "[Search Company By Name] '" + src + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    label6.Text = dr.GetValue(0).ToString();
                    comboBox1.Text = dr.GetValue(1).ToString();
                    textBox6.Text = dr.GetValue(2).ToString();
                    textBox5.Text = dr.GetValue(3).ToString();
                }
            }
            else
                MessageBox.Show("Company Not Found");
            DBcon.con().Close();
        }

        //DELETE
        private void button2_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //Company
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                isVal = false;
                errorProviderName.SetError(textBox1, "Company Name Required");
            }
            else
            {
                errorProviderName.Clear();
                try
                {

                    string qur = "SELECT * FROM dbo.Company WHERE Cname = '" + textBox1.Text + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        isVal = false;
                        errorProviderName.SetError(textBox1, "Enter valid Company Name");
                    }
                    else
                        errorProviderName.Clear();
                }
                catch
                {
                    isVal = false;
                    errorProviderName.SetError(textBox1, "Company Name Required");
                };
            }
            //Del
            if (textBox1.Text != "" && isVal)
            {

                if (MessageBox.Show("Are you sure you want to delete record?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string dname = textBox1.Text;
                    string qur = "[DELETE Company] '" + dname + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    cmd.ExecuteNonQuery();
                    DBcon.con().Close();
                    MessageBox.Show("Deleted Successfully");
                }
            }
            else
                MessageBox.Show("Please enter a valid Company name");
        }

        //DISPLAY
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string qur = "[Display Companies]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            DBcon.con().Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
