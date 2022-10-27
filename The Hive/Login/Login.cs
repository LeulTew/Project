using Login.AdminForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
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

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogReg l=new LogReg();
            l.Show();
            this.Close();
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
            //DBcon dBcon = new DBcon();
            bool isVal = true;
            try
            {
                Regex regex = new Regex(@"^[a-z]{2}$");
                errorProviderName.Clear();
                errorProviderPass.Clear();
                if (regex.IsMatch(textBox1.Text))
                {
                    isVal = false;
                    errorProviderName.SetError(textBox1, "2 digit name");
                }
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    isVal = false;
                    errorProviderName.SetError(textBox1, "Name is Required");
                }
                else
                    errorProviderName.Clear();
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    isVal = false;
                    errorProviderPass.SetError(textBox2, "Password is Required");
                }
                else
                    errorProviderPass.Clear();
                if (isVal)
                {
                    string query = "SELECT * FROM Customer WHERE Username = '" + textBox1.Text + "' AND Pass ='" + textBox2.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, DBcon.con());

                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        username = textBox1.Text;
                        password = textBox2.Text;

                        // page needed to be loaded next
                        //create obj then .Show() it
                        //last this.Hide()
                        Transfer t = new Transfer
                        {
                            usern = textBox1.Text,
                            pass = textBox2.Text,
                        };
                        t.trans();
                        Main m = new Main();
                        m.Show();
                        this.Close();
                    }
                    else
                    {
                        /*MessageBox.Show("Invalid login details","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        textBox1.Clear();
                        textBox2.Clear();
                        */
                        errorProviderName.SetError(textBox1, "Invalid Login Detail");
                        errorProviderPass.SetError(textBox2, "Invalid Login Detail");
                        //to focus on username
                        textBox1.Focus();
                        textBox1.BackColor = Color.White;
                        panel3.BackColor = Color.White;
                        panel4.BackColor = SystemColors.ControlLight;
                        textBox2.BackColor = SystemColors.ControlLight;
                    }
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
            string username, password;
            username = textBox1.Text;
            password = textBox2.Text;
            DBcon dBcon = new DBcon();
            bool isVal = true;
            try
            {
                Regex regex = new Regex(@"^[a-z]{2}$");
                errorProviderName.Clear();
                errorProviderPass.Clear();
                if (regex.IsMatch(textBox1.Text))
                {
                    isVal = false;
                    errorProviderName.SetError(textBox1, "2 digit name");
                }
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    isVal = false;
                    errorProviderName.SetError(textBox1, "Name is Required");
                }
                else
                    errorProviderName.Clear();
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    isVal = false;
                    errorProviderPass.SetError(textBox2, "Password is Required");
                }
                else
                    errorProviderPass.Clear();
                if (isVal)
                {
                    string query = "SELECT * FROM Adminstrator WHERE Username = '" + textBox1.Text + "' AND Pass ='" + textBox2.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, DBcon.con());
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        username = textBox1.Text;
                        password = textBox2.Text;

                        // page needed to be loaded next
                        //create obj then .Show() it
                        //last this.Hide()
                        Amain a = new Amain();
                        a.Show();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            catch
            {
                MessageBox.Show("ERROR - No accounts found");
            }
            
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
