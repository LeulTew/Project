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
    public partial class ItemM : Form
    {
        public ItemM()
        {
            InitializeComponent();
        }
        //DBcon c = new DBcon();
        //SqlConnection con = new SqlConnection(@"Data Source = EMPEROR\SQLEXPRESS;Initial Catalog =Empinfo;Integrated Security =true");
        string imageloc = "";

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg) || PNG files(*.png) || All files(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageloc = dialog.FileName;
                    AddImage.ImageLocation = imageloc;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
        public int getCBid()
        {
            string cm = txtItemCat.Text;
            int cd = 0;           
            if (cm == "Respiratory & Allergies")
            {
                cd = 2;
            }
            else if (cm == "Eye & EarCare")
            {
                cd = 3;
            }
            else if (cm == "Foot & LegCare")
            {
                cd = 4;
            }
            else if (cm == "Oral Care")
            {
                cd = 5;
            }
            else if (cm == "Digestive Care")
            {
                cd = 6;
            }
            else if (cm == "Skin & Scalp Care")
            {
                cd = 7;
            }
            else if (cm == "Health Supplements")
            {
                cd = 8;
            }
            else if (cm == "Natural & Homeopathic")
            {
                cd = 9;
            }
            else if (cm == "Personal Aids & Repellents")
            {
                cd = 10;
            }
            else if (cm == "Sleep & Stress Relief")
            {
                cd = 11;
            }
            else if (cm == "Family Planning")
            {
                cd = 12;
            }
            else if (cm == "Habit Treatment")
            {
                cd = 13;
            }
            else if (cm == "First Aid")
            {
                cd = 14;
            }
            else if (cm == "Baby Treatments")
            {
                cd = 15;
            }
            else if (cm == "Drug Administration")
            {
                cd = 16;
            }
            else if (cm == "Medical Devices")
            {
                cd = 17;
            }
            else if (cm == "Travel")
            {
                cd = 18;
            }
            else if (cm == "Bladder,Genetal & Rectal")
            {
                cd = 19;
            }
            else
            {
                cd = 1;
            }
            return cd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //Name
            if (string.IsNullOrEmpty(AddName.Text))
            {
                isVal = false;
                errorProviderName.SetError(AddName, "Name is Required");
            }
            else
                errorProviderName.Clear();
            //Item Desc
            if (string.IsNullOrEmpty(AddDesc.Text))
            {
                isVal = false;
                errorProviderDescription.SetError(AddDesc, "Inventory Number is Required");
            }
            else
                errorProviderDescription.Clear();
            //Item Cat
            if (string.IsNullOrEmpty(txtItemCat.Text))
            {
                isVal = false;
                epItemCategory.SetError(txtItemCat, "Number is Required");
            }
            else
                epItemCategory.Clear();
            //Quantity
            if (string.IsNullOrEmpty(AddQty.Text))
            {
                isVal = false;
                errorCount.SetError(AddQty, "Count is Required");
            }
            else
                errorCount.Clear();
            try
            {
                errorCount.Clear();
                //int cb=getCBid();
                if (int.Parse(AddQty.Text) > 0)
                    IsAvail.AutoCheck = true;
                else
                    IsAvail.AutoCheck = false;
            }
            catch
            {
                errorCount.SetError(AddQty, "Count is required");
            }
            //Price
            if (string.IsNullOrEmpty(AddPrice.Text))
            {
                isVal = false;
                errorProviderPrice.SetError(AddPrice, "Price is required");
            }
            else
                errorCount.Clear();
            try
            {
                errorProviderPrice.Clear();
                double.Parse(AddPrice.Text);
            }
            catch (Exception)
            {
                errorProviderPrice.SetError(AddPrice, "Price is required");
            }
            //image
            string img;
            if (AddImage.ImageLocation == null)
                img = "";
            else
                img = AddImage.ImageLocation;

            //Add
            if (isVal)
            {
                Prod i1 = new Prod
                {
                    Name = AddName.Text,
                    Price = double.Parse(AddPrice.Text),
                    imageLoc = img,
                    Description = AddDesc.Text,
                    Quantity = int.Parse(AddQty.Text),
                    ExpireDate = AddExp.Value,
                    CategortyName = txtItemCat.Text,
                };
                try
                {
                    int rowsAffected = i1.save();
                    if (rowsAffected > 0)
                        MessageBox.Show("Item Added");
                    else
                        MessageBox.Show("Item Not Added");
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }

            }
        }

        private void ItemM_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'empinfoDataSet.ItemCategory' table. You can move, or remove it, as needed.
           
            // TODO: This line of code loads data into the 'empinfoDataSet1.ItemCategory' table. You can move, or remove it, as needed.
             string qur = "Select CategoryName FROM ItemCategory";
             SqlCommand cmd = new SqlCommand(qur, DBcon.con());
             SqlDataAdapter sdr = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             sdr.Fill(dt);
             txtItemCat.DataSource = dt;
             cbEditCat.DataSource = dt;
             txtItemCat.DisplayMember = "CatagoryName";
             txtItemCat.ValueMember = "Id";
             DBcon.con().Close();

            string qurr = "[Display All Item]";
            SqlCommand cmdd = new SqlCommand(qurr, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmdd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable ddt = new DataTable();
            sd.Fill(ddt);
            dataGridView1.DataSource = ddt;
            DBcon.con().Close();
            

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string qur = "[Display All Item]";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            sd.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            DBcon.con().Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //ItemName
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                isVal = false;
                errorProviderName.SetError(textBox1, "ItemName Required");
            }
            else
            {
                errorProviderName.Clear();
                try
                {

                    string qur = "SELECT * FROM dbo.Item WHERE ItemName = '" + textBox1.Text + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        isVal = false;
                        errorProviderName.SetError(textBox1, "Enter valid Item Name");
                    }
                    else
                        errorProviderName.Clear();
                }
                catch
                {
                    isVal = false;
                    errorProviderName.SetError(textBox1, "Item Name Required");
                };
            }
            //Delete
            if (textBox1.Text != "" && isVal)
            {
                if (MessageBox.Show("Are you sure you want to delete item?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string dname = textBox1.Text;
                    string qur = "[DELETE Item] '" + dname + "'";
                    SqlCommand cmd = new SqlCommand(qur, DBcon.con());
                    cmd.ExecuteNonQuery();
                    DBcon.con().Close();
                    MessageBox.Show("Deleted Successfully");
                    textBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Item Name");
            }
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string src = txtEditName.Text;
            string qur = "[Search Item By Name] '" + src + "'";
            SqlCommand cmd = new SqlCommand(qur, DBcon.con());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblEditID.Text = dr.GetValue(0).ToString();
                txtEditName.Text = dr.GetValue(1).ToString();
                txtEditPrice.Text = dr.GetValue(2).ToString();
                EditImage.ImageLocation = dr.GetValue(3).ToString();
                txtEditDesc.Text = dr.GetValue(4).ToString();
                txtEditQty.Text = dr.GetValue(5).ToString();
                EditExp.Value = (DateTime)dr[6];
                cbEditIsAvail.Checked = (bool)dr[7] ;
                string q = "Select CategoryName FROM ItemCategory WHERE Id = '" + (int)dr[8] + "'";
                SqlCommand c = new SqlCommand(q, DBcon.con());
                cbEditCat.Text = c.ExecuteScalar().ToString();
            }
            DBcon.con().Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg) || PNG files(*.png) || All files(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageloc = dialog.FileName;
                    EditImage.ImageLocation = imageloc;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool isVal = true;
            //Name
            if (string.IsNullOrEmpty(txtEditName.Text))
            {
                isVal = false;
                errorProviderName.SetError(txtEditName, "Name is Required");
            }
            else
                errorProviderName.Clear();
            //Item Desc
            if (string.IsNullOrEmpty(txtEditDesc.Text))
            {
                isVal = false;
                errorProviderDescription.SetError(txtEditDesc, "Inventory Number is Required");
            }
            else
                errorProviderDescription.Clear();
            //Item Cat
            if (string.IsNullOrEmpty(cbEditCat.Text))
            {
                isVal = false;
                epItemCategory.SetError(cbEditCat, "Cat is Required");
            }
            else
                epItemCategory.Clear();
            //Quantity
            if (string.IsNullOrEmpty(txtEditQty.Text))
            {
                isVal = false;
                errorCount.SetError(txtEditQty, "Count is Required");
            }
            else
                errorCount.Clear();
            try
            {
                errorCount.Clear();
                //int cb=getCBid();
                if (int.Parse(txtEditQty.Text) > 0)
                    IsAvail.AutoCheck = true;
                else
                    IsAvail.AutoCheck = false;
            }
            catch
            {
                errorCount.SetError(txtEditQty, "Count is required");
            }
            //Price
            if (string.IsNullOrEmpty(txtEditPrice.Text))
            {
                isVal = false;
                errorProviderPrice.SetError(txtEditPrice, "Price is required");
            }
            else
                errorCount.Clear();
            try
            {
                errorProviderPrice.Clear();
                double.Parse(txtEditPrice.Text);
            }
            catch (Exception)
            {
                errorProviderPrice.SetError(txtEditPrice, "Price is required");
            }
            //image
            string img;
            if (EditImage.ImageLocation == null)
                img = "";
            else
                img = EditImage.ImageLocation;

            //Edit
            if (isVal)
            {
                int rid = int.Parse(lblEditID.Text);
                Prod i1 = new Prod
                {
                    Id = rid,
                    Name = txtEditName.Text,
                    Price = double.Parse(txtEditPrice.Text),
                    imageLoc = img,
                    Description = txtEditDesc.Text,
                    Quantity = int.Parse(txtEditQty.Text),
                    ExpireDate = EditExp.Value,
                    CategortyName = cbEditCat.Text,
                };
                try
                {
                    int rowsAffected = i1.update();
                    if (rowsAffected > 0)
                        MessageBox.Show("Updated Successfully");
                    else
                        MessageBox.Show("Update Failed");
                }
                catch
                {
                    MessageBox.Show("Update Error");
                }
            }
        }
    }
}
