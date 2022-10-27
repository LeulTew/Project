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
    public partial class prof : UserControl
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; label14.Text = _id.ToString(); }
        }
        private string _name;
        public string NAME
        {
            get { return _name; }
            set { _name = value; label15.Text = _name; }
        }
        private string _email;
        public string EMAIL
        {
            get { return _email; }
            set { _email = value; label16.Text = _email; }
        }
        private DateTime _date;
        public DateTime DATE
        {
            get { return _date; }
            set { _date = value; label17.Text = _date.ToString(); }
        }
        private string _user;
        public string USERN
        {
            get { return _user; }
            set { _user = value; label18.Text = _user; }
        }
        private string _pass;
        public string PASSWORD
        {
            get { return _pass; }
            set { _pass = value; label19.Text = _pass; }
        }
        public prof()
        {
            InitializeComponent();
        }
    }
}
