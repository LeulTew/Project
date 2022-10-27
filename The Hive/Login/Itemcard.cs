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
    public partial class Itemcard : UserControl
    {
        private string _name;
        public string NAME
        {
            get { return _name; }
            set { _name = value; label3.Text = _name; }
        }
        private double _price;
        public double MONEY
        {
            get { return _price; }
            set { _price = value; label4.Text =_price.ToString(); }
        }
        private string _pic;
        public string PIC
        {
            get { return _pic; }
            set { _pic = value; image1.ImageLocation = _pic; }
        }
        public Itemcard()
        {
            InitializeComponent();
        }
    }
}
