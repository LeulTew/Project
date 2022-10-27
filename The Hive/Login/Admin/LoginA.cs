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

namespace Login
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = EMPEROR\SQLEXPRESS;Initial Catalog =Empinfo;Integrated Security =true");
        private void pic1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox2.UseSystemPasswordChar =true;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.ControlLight;
            textBox2.BackColor = SystemColors.ControlLight;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.White;
            panel4.BackColor = Color.White;
            panel3.BackColor = SystemColors.ControlLight;
            textBox1.BackColor = SystemColors.ControlLight;

        }

        private void log_Click(object sender, EventArgs e)
        {
            string username, password;
            username=textBox1.Text;
            password=textBox2.Text;

            try
            {
                string query = "SELECT * FROM Adminlog WHERE usern = '" + textBox1.Text + "' AND passw ='" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query,con);

                DataTable dt = new DataTable();
                sda.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    username = textBox1.Text;
                    password = textBox2.Text;

                    // page needed to be loaded next
                    //create obj then .Show() it
                    //last this.Hide()
                    MessageBox.Show("You have Connected succesfully");
                }
                else
                {
                    MessageBox.Show("Invalid login details","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();

                    //to focus on username
                    textBox1.Focus();
                    textBox1.BackColor = Color.White;
                    panel3.BackColor = Color.White;
                    panel4.BackColor = SystemColors.ControlLight;
                    textBox2.BackColor = SystemColors.ControlLight;
                }
            }
            catch
            {
                MessageBox.Show("ERROR - No accounts found");
            }
            finally
            {

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
    }
}
