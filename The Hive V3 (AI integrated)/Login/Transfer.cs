using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    internal class Transfer
    {
        private static List<Transfer> users = new List<Transfer>();
        public string usern { get; set; }
        public string pass  { get; set; }
        public void trans()
        {
            Console.WriteLine("Saved");
            users.Add(this);
        }
        public static List<Transfer> getuser()
        {
            return users;
        }
        
        public string getpass()
        {
            return pass;
        }
    }
}
