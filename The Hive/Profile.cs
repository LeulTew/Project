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
using System.Text.RegularExpressions;

namespace Login
{
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();
        }
        string pid = "";

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;

            string username = "";
            foreach (var item in Transfer.getuser())
            {
                username = item.usern;
            }
            User current = User.findUser(username);
            labelID.Text = current.ID;
            txtFN.Text = current.FName;
            txtLN.Text = current.LName;
            txtEmail.Text = current.Email;
            dateTimePicker1.Value = current.Dob;
            txtGender.Text = current.Gender;
            txtUN.Text = current.UserName;
            txtPass.Text = current.Password;
            pid = labelID.Text;
            label25.Text = pid;
            DBcon.con().Close();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            displayprof();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //First Name
            if (string.IsNullOrEmpty(txtFN.Text))
            {
                isVal = false;
                epFName.SetError(txtFN, "Name is Required");
            }
            else
                epFName.Clear();

            //Last Name
            if (string.IsNullOrEmpty(txtLN.Text))
            {
                isVal = false;
                epLName.SetError(txtLN, "Name is Required");
            }
            else
                epLName.Clear();

            //Email
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                isVal = false;
                epEmail.SetError(txtEmail, "Email is Required");
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                isVal = false;
                epEmail.SetError(txtEmail, "Enter a valid Email");
            }
            else
                epEmail.Clear();

            //DoB
            if (string.IsNullOrEmpty(dateTimePicker1.Text))
            {
                isVal = false;
                epDob.SetError(dateTimePicker1, "Dob is Required");
            }
            else if ((int)((DateTime.Now - dateTimePicker1.Value).TotalDays / 365.242199) < 18)
            {
                isVal = false;
                epDob.SetError(dateTimePicker1, "Should be above 18");
            }
            else
                epDob.Clear();

            //Gender
            if (string.IsNullOrEmpty(txtGender.Text) || (txtGender.Text != "Male" && txtGender.Text != "Female"))
            {
                isVal = false;
                epGender.SetError(txtGender, "Enter Gender (Male or Female)");
            }
            else
                epGender.Clear();

            //User
            if (string.IsNullOrEmpty(txtUN.Text))
            {
                isVal = false;
                epUser.SetError(txtUN, "UserName Required");
            }
            else
            {
                epUser.Clear();
                try
                {
                    var Uname = User.findUser(txtUN.Text);
                    if (Uname != null)
                        if (Uname.UserName != txtUN.Text)
                        {
                            isVal = false;
                            epUser.SetError(txtUN, "User Name already Exists");
                        }
                        else
                            epUser.Clear();
                }
                catch
                {
                    isVal = false;
                    epUser.SetError(txtUN, "UserName Required");
                };
            }
            if (isVal)
            {
                User u1 = new User
                {
                    ID = pid,
                    UserName = txtUN.Text,
                    Password = txtPass.Text,
                    FName = txtFN.Text,
                    LName = txtLN.Text,
                    Gender = txtGender.Text,
                    Email = txtEmail.Text,
                    Dob = dateTimePicker1.Value,
                };
                try
                {
                    int rowsAffected = u1.update(pid);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Updated Successfully");
                    }
                    else
                        MessageBox.Show("Update Failed");
                }
                catch
                {
                    MessageBox.Show("Update Error");
                }
                displayprof();
                panel1.Visible = true;
                panel2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
            DBcon.con().Close();
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void displayprof()
        {
            string username = "";
            foreach (var item in Transfer.getuser())
            {
                username = item.usern;
            }
            User current = User.findUser(username);
            labelID.Text = current.ID;
            labelFName.Text = current.FName;
            labelLName.Text = current.LName;
            labelEmail.Text = current.Email;
            labelDob.Text = current.Dob.ToString();
            labelGender.Text = current.Gender;
            labelUserN.Text = current.UserName;
            labelPass.Text = current.Password;
            pid = labelID.Text;
            label25.Text = pid;
            DBcon.con().Close();
        }

        private void showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (showPass.Checked)
                labelPass.Visible = true;
            else
                labelPass.Visible = false;
        }
    }
}
