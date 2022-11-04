using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Login
{
    public partial class splash : Form
    {


        public splash()
        {

            InitializeComponent();
            
        }
       
        
        private void timer1_Tick(object sender, EventArgs e)
        {
           timer1.Enabled = true;
            progressBar1.Increment(10);
            if(progressBar1.Value == 100)
            {
                timer1.Enabled=false;
                Login ll =new Login();
                ll.Show();
                this.Hide();
            }
        }

        private void splash_Load(object sender, EventArgs e)
        {
            DBcon d =new DBcon();
        }
    }
}
