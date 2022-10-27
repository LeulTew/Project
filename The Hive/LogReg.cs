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
    public partial class LogReg : Form
    {
        public LogReg()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        public string idgen()
        {
            string oid = "AB";
            Random rand = new Random();
            string no = rand.Next(0, 9999).ToString("D4");
            string nid = oid + no;
            return nid;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //First Name
            if (string.IsNullOrEmpty(Fname.Text))
            {
                isVal = false;
                epFName.SetError(Fname, "Name is Required");
            }
            else
                epFName.Clear();

            //Last Name
            if (string.IsNullOrEmpty(Lname.Text))
            {
                isVal = false;
                epLName.SetError(Lname, "Name is Required");
            }
            else
                epLName.Clear();

            //Email
            if (string.IsNullOrEmpty(email.Text))
            {
                isVal = false;
                epEmail.SetError(email, "Email is Required");
            }
            else
                epEmail.Clear();

            //DoB
            if(string.IsNullOrEmpty(BirthDate.Text))
            {
                isVal = false;
                epDob.SetError(BirthDate, "Dob is Required");
            }else if ((int)((DateTime.Now - BirthDate.Value).TotalDays / 365.242199) < 18)
            {
                isVal = false;
                epDob.SetError(BirthDate, "Should be above 18 to register");
            }
            else
                epDob.Clear();

            //Gender
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                isVal = false;
                epGender.SetError(radioButton2, "Choose Gender");
            }
            else
                epGender.Clear();

            //User
            if (string.IsNullOrEmpty(txtUserN.Text))
            {
                isVal = false;
                epUser.SetError(txtUserN, "UserName Required");
            }
            else
            {
                epUser.Clear();
                try
                {
                    var Uname = User.findUser(txtUserN.Text);
                    if (Uname != null)
                    {
                        isVal = false;
                        epUser.SetError(txtUserN, "User Name already Exists");
                    }
                    else
                        epUser.Clear();
                }
                catch
                {
                    isVal = false;
                    epUser.SetError(txtUserN, "UserName Required");
                };
            }
            

                //REg
                if (isVal)
            {
                User u1 = new User
                {
                    //ID = idgen(),
                    UserName = txtUserN.Text,
                    Password = pass.Text,
                    FName = Fname.Text,
                    LName = Lname.Text,
                    Gender = radioButton1.Checked ? "Male" : "Female",
                    Email = email.Text,
                    Dob = BirthDate.Value,
                };
                try
                {
                    int rowsAffected = u1.save();
                    if (rowsAffected > 0)
                        MessageBox.Show("Register Complete");
                    else
                        MessageBox.Show("Registration Failed");
                    Clear();
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
        }
        private void Clear()
        {
            Fname.Text= "";
            Lname.Text = "";
            email.Text= "";
            txtUserN.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            pass.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Login l = new Login();
            l.Show();
            
        }

        private void LogReg_Load(object sender, EventArgs e)
        {

        }
    }
}
