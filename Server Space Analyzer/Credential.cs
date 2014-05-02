using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_Space_Analyzer
{
    public class Credential
    {
        private String my_username;
        private String my_password;

        public Credential(String the_username, String the_password)
        {
            my_username = the_username;
            my_password = the_password;
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
