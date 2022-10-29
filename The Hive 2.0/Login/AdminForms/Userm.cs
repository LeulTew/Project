
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

namespace Login.AdminForms
{
    public partial class Userm : Form
    {
        public Userm()
        {
            InitializeComponent();
        }
        public string idgen()
        {
            string oid = "AB";
            Random rand = new Random();
            string no =rand.Next(0, 9999).ToString("D4");
            string nid =oid+no;
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
            else if (!Regex.IsMatch(email.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                isVal = false;
                epEmail.SetError(email, "Enter a valid Email");
            }
            else
                epEmail.Clear();

            //DoB
            if (string.IsNullOrEmpty(BirthDate.Text))
            {
                isVal = false;
                epDob.SetError(BirthDate, "Dob is Required");
            }
            else if ((int)((DateTime.Now - BirthDate.Value).TotalDays / 365.242199) < 18)
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
                epUser.Clear();
            try
            {
                var Uname = User.findUser(txtUserN.Text);
                if (Uname != null)
                    if( Uname.UserName == txtUserN.Text)
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
            }
            //Pass
            if (string.IsNullOrEmpty(pass.Text))
            {
                isVal = false;
                epPass.SetError(pass, "Password Required");
            }
            else if (!Regex.IsMatch(pass.Text, "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$"))
            {
                MessageBox.Show("Password must be atleast 8 to 15 characters. It contains atleast one Upper case and numbers.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pass.Focus();
                epPass.SetError(pass, "Valid Password Required");
            }
            else
                epPass.Clear();

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
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string name = txtEditUserN.Text;
            string qur = "[Search User By Username] '" + name + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lblID.Text = dr.GetValue(0).ToString();
                    txtEditFirstN.Text = dr.GetValue(3).ToString();
                    txtEditLastN.Text = dr.GetValue(4).ToString();
                    txtEditEmail.Text = dr.GetValue(6).ToString();
                    EditDob.Value = (DateTime)dr[5];
                    txtEditGender.Text = dr.GetValue(8).ToString();
                    txtEditPass.Text = dr.GetValue(2).ToString();
                    txtEditUserN.Text = dr.GetValue(1).ToString();
                }
            }
            else
                MessageBox.Show("User Not Found");
            DBcon.con().Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //First Name
            if (string.IsNullOrEmpty(txtEditFirstN.Text))
            {
                isVal = false;
                epFName.SetError(txtEditFirstN, "Name is Required");
            }
            else
                epFName.Clear();

            //Last Name
            if (string.IsNullOrEmpty(txtEditLastN.Text))
            {
                isVal = false;
                epLName.SetError(txtEditFirstN, "Name is Required");
            }
            else
                epLName.Clear();

            //Email
            if (string.IsNullOrEmpty(txtEditEmail.Text))
            {
                isVal = false;
                epEmail.SetError(txtEditEmail, "Email is Required");
            }
            else if (!Regex.IsMatch(txtEditEmail.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                isVal = false;
                epEmail.SetError(txtEditEmail, "Enter a valid Email");
            }
            else
                epEmail.Clear();

            //DoB
            if (string.IsNullOrEmpty(EditDob.Text))
            {
                isVal = false;
                epDob.SetError(EditDob, "Dob is Required");
            }
            else if ((int)((DateTime.Now - EditDob.Value).TotalDays / 365.242199) < 18)
            {
                isVal = false;
                epDob.SetError(EditDob, "Should be above 18");
            }
            else
                epDob.Clear();

            //Gender
            if (string.IsNullOrEmpty(txtEditGender.Text) || (txtEditGender.Text != "Male" && txtEditGender.Text != "Female"))
            {
                isVal = false;
                epGender.SetError(txtEditGender, "Enter Gender (Male or Female)");
            }
            else
                epGender.Clear();

            //User
            if (string.IsNullOrEmpty(txtEditUserN.Text))
            {
                isVal = false;
                epUser.SetError(txtEditUserN, "UserName Required");
            }
            else
            {
                epUser.Clear();
                try
                {
                    var Uname = User.findUser(txtEditUserN.Text);
                    if (Uname != null)
                        if (Uname.UserName != txtEditUserN.Text && Uname.ID != lblID.Text)
                        {
                            isVal = false;
                            epUser.SetError(txtEditUserN, "User Name already Exists");
                        }
                        else
                            epUser.Clear();
                }
                catch
                {
                    isVal = false;
                    epUser.SetError(txtEditUserN, "UserName Required");
                };
            }
            //Pass
            if (string.IsNullOrEmpty(txtEditPass.Text))
            {
                isVal = false;
                epPass.SetError(txtEditPass, "Password Required");
            }
            else if (!Regex.IsMatch(txtEditPass.Text, "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$"))
            {
                MessageBox.Show("Password must be atleast 8 to 15 characters. It contains atleast one Upper case and numbers.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                pass.Focus();
                epPass.SetError(txtEditPass, "Valid Password Required");
            }
            else
                epPass.Clear();

            //Edit
            if (isVal)
            {
                string rid = lblID.Text;
                User u1 = new User
                {
                    ID = rid,
                    UserName = txtEditUserN.Text,
                    Password = txtEditPass.Text,
                    FName = txtEditFirstN.Text,
                    LName = txtEditLastN.Text,
                    Gender = txtEditGender.Text,
                    Email = txtEditEmail.Text,
                    Dob = EditDob.Value,
                };
                try
                {
                    int rowsAffected = u1.update(rid);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Updated Successfully");
                        txtEditFirstN.Text = "";
                        txtEditLastN.Text = "";
                        txtEditEmail.Text = "";
                        txtEditGender.Text = "";
                        txtEditUserN.Text = "";
                        txtEditPass.Text = "";
                        txtEditUserN.Focus();
                    }
                    else
                        MessageBox.Show("Update Failed");
                }
                catch
                {
                    MessageBox.Show("Update Error");
                }
                /*   try
               {
                   
                   string qur = "[Update User] '" + rid + "', '" + txtEditUserN.Text + "', '" + txtEditPass.Text + "', '" + txtEditFirstN.Text + "', '" + txtEditLastN.Text + "', '" + DateTime.Parse(EditDob.Text) + "', '" + txtEditEmail.Text + "', '" + txtEditGender.Text + "'";
                   SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                   cmd.ExecuteNonQuery();
                   DBcon.con().Close();
                   MessageBox.Show("Updated Successfully");
               }
               catch{
                   MessageBox.Show("Update Error");
               }*/
            }
            
        }

        private void Userm_Load(object sender, EventArgs e)
        {
            string dname = textBox1.Text;
            string qur = "[Display All User]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            DBcon.con().Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string dname = textBox1.Text;
            string qur = "[Display All User]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            DBcon.con().Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string name = txtEditEmail.Text;
            string qur = "[Search User By Email] '" + name + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lblID.Text = dr.GetValue(0).ToString();
                    txtEditFirstN.Text = dr.GetValue(3).ToString();
                    txtEditLastN.Text = dr.GetValue(4).ToString();
                    txtEditEmail.Text = dr.GetValue(6).ToString();
                    EditDob.Value = (DateTime)dr[5];
                    txtEditGender.Text = dr.GetValue(8).ToString();
                    txtEditPass.Text = dr.GetValue(2).ToString();
                    txtEditUserN.Text = dr.GetValue(1).ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                    
                    string qur = "SELECT * FROM HiveUser.Customer WHERE Username = '" + textBox1.Text + "'";
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
            //Del
            if (textBox1.Text != "" && isVal)
            {

                if (MessageBox.Show("Are you sure you want to delete record?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string dname = textBox1.Text;
                    string qur = "[DELETE User] '" + dname + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    cmd.ExecuteNonQuery();
                    DBcon.con().Close();
                    MessageBox.Show("Deleted Successfully");
                }
            }
            else
                MessageBox.Show("Please enter a valid Username");
        }
    }
}