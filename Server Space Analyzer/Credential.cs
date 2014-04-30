using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Disk_Space_Analyzer
{
    public class Credential
    {
        private String my_username;
        private String my_password;

        public Credential()
        {
            Console.Out.Write("Username:");
            my_username = Console.ReadLine();
            Console.Out.Write("Password:");
            my_password = Console.ReadLine();
        }

        public String getUsername()
        {
            return my_username;
        }

        public String getPassword()
        {
            return my_password;
        }
    }
}
