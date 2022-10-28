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
    public partial class Item : Form
    {

        public Item()
        {
            InitializeComponent();
        }

        private void Item_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var item in Prod.getitem())
            {
                Itemcard card = new Itemcard();
                card.PIC = item.imageLoc;
                
                card.NAME = item.Name;
                card.MONEY = item.Price;               
                flowLayoutPanel1.Controls.Add(card);
            }
            flowLayoutPanel2.Controls.Clear();
            foreach (var item in Prod.getOther())
            {
                Itemcard card = new Itemcard();
                card.PIC = item.imageLoc;

                card.NAME = item.Name;
                card.MONEY = item.Price;
                flowLayoutPanel2.Controls.Add(card);
            }
        }
    }
}
