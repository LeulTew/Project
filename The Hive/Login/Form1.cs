using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string imageloc = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg) || PNG files(*.png) || All files(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageloc = dialog.FileName;
                    pictureBox1.ImageLocation=imageloc;
                }
            }
            catch(Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
               
               

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Prod m1 = new Prod
            {
                
                Name = textBox1.Text,
                Price = int.Parse(textBox2.Text),
                imageLoc = pictureBox1.ImageLocation,
                
            };
            m1.save();
            MessageBox.Show("You have registered Succesfully");
        }
    }
}
