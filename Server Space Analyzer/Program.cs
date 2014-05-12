using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Space_Analyzer
{
    static class Program
    {
        private static Credential my_credential;
        private static Credential my_temporary_credential;

        private static List<String> servers = new List<String>();
        private static int index;
        private static bool my_temporary;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            LoginForm form = new LoginForm();
            Application.Run(form);

            System.Diagnostics.Process.Start(@"Server Spaces.xlsx");
        }

        public static void scan()
        {
            while (index < servers.Count())
            {

            }
        }

        public static void setCredential(Credential credential, bool temporary)
        {
            if (temporary)
            {
                my_temporary_credential = credential;
                my_temporary = true;
            }
            else
            {
                my_credential = credential;
            }
        }
    }
}