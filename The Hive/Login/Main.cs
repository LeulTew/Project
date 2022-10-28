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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        int x=0;
        private void button6_Click(object sender, EventArgs e)
        {
            
            if (x%2==0)
            {
                pictureBox1.Width = 130;
                panel1.Width = 130;
                button1.Width = 100;
                button1.ImageAlign = ContentAlignment.MiddleLeft;
                button1.Text="     Profile";
                button2.Width = 100;
                button2.ImageAlign = ContentAlignment.MiddleLeft;
                button2.Text = "     Home";
                button3.Width = 100;
                button3.ImageAlign = ContentAlignment.MiddleLeft;
                button3.Text = "     Items";
                button4.Width = 100;
                button4.ImageAlign = ContentAlignment.MiddleLeft;
                button4.Text = "     Shop";
                button5.Width = 100;
                button5.ImageAlign = ContentAlignment.MiddleLeft;
                button5.Text = "     Exit";
                x++;
            }
            else
            {
                panel1.Width = 72;
                button1.Width = 43;
                button1.Text = "";
                button1.ImageAlign = ContentAlignment.MiddleCenter;
                button2.Width = 43;
                button2.Text = "";
                button2.ImageAlign = ContentAlignment.MiddleCenter;
                button3.Width = 43;
                button3.Text = "";
                button3.ImageAlign = ContentAlignment.MiddleCenter;
                button4.Width = 43;
                button4.Text = "";
                button4.ImageAlign = ContentAlignment.MiddleCenter;
                button5.Width = 43;
                button5.Text = "";
                button5.ImageAlign = ContentAlignment.MiddleCenter;
                x++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var activeScreen = ActiveMdiChild;
            if (activeScreen != null)
            {
                activeScreen.Close();
            }
            Profile screen = new Profile();
            screen.MdiParent = this;
            screen.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var activeScreen = ActiveMdiChild;
            if (activeScreen != null)
            {
                activeScreen.Close();
            }
            Item screen = new Item();
            screen.MdiParent = this;
            screen.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var activeScreen = ActiveMdiChild;
            if (activeScreen != null)
            {
                activeScreen.Close();
            }
            Order screen = new Order();
            screen.MdiParent = this;
            screen.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var activeScreen = ActiveMdiChild;
            if (activeScreen != null)
            {
                activeScreen.Close();
            }
            Homepage screen = new Homepage();
            screen.MdiParent = this;
            screen.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
