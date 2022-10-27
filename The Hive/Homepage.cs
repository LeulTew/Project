using Login.Properties;
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
    public partial class Homepage : Form
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            
            
        }
        int img = 0;
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            if (img >3)
            {
                img = 0;
            }
            else
            {
                img++;
            }
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.Image = imgg(img);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            if (img <= 0)
            {
                img = 4;
            }
            else
            {
                img--;
            }
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.Image = imgg(img);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (img > 3)
            {
                img = 0;
            }
            else
            {
                img++;
            }
            pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox9.Image = imgg(img);
            
        }
       
        public Image imgg(int x)
        {
            if (x == 0)
            {
                Image image1 = Resources._11;
                return image1;
            }
            else if (x == 1)
            {
                Image image2 = Resources._2;
                return image2;
            }
            else if (x == 2)
            {
                Image image3 = Resources._3;
                return image3;
            }
            else if(x== 3)
            {
                Image image4 = Resources._4;
                return image4;
            }
            else
            {
                Image image5 = Resources._11;
                return image5;
            }
        }
    }
}
