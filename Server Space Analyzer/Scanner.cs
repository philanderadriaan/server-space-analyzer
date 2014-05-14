using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Space_Analyzer
{
    class Scanner
    {
        public event EventHandler SomethingHappened;

        private String my_server;
        private Credential my_credential;
        private Formatter my_formatter = new Formatter();
        private List<string> errors = new List<string>();


        public Scanner(Credential the_credential)
        {
            my_credential = the_credential;
        }

        public string getServer()
        {
            return my_server;
        }

        public List<List<string>> scan(string the_server)
        {
            my_server = the_server;
            EventHandler handler = SomethingHappened;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            List<List<string>> data = new List<List<string>>();
            try
            {
                ConnectionOptions options = new ConnectionOptions();
                options.Username = my_credential.getUsername();
                options.Password = my_credential.getPassword();
                string name = @"\\";
                if (the_server != "")
                {
                    name += the_server;
                }
                else
                {
                    name += ".";
                }
                name += @"\root\cimv2";
                ManagementObjectCollection collection = new ManagementObjectSearcher(new ManagementScope(name, options), new ObjectQuery("select VolumeName, Name, Size, Freespace from Win32_LogicalDisk where DriveType=3")).Get();

                bool first = true;
                data.Add(new List<string>());
                foreach (ManagementObject i in collection)
                {
                    List<string> row = new List<string>();
                    if (first)
                    {
                        row.Add(the_server);
                        first = false;
                    }
                    else
                    {
                        row.Add("");
                    }
                    row.Add(my_formatter.formatName(i["VolumeName"].ToString(), i["Name"].ToString()));
                    row.Add(my_formatter.formatCapacity(i["Size"].ToString()));
                    row.Add(my_formatter.formatCapacity(i["FreeSpace"].ToString()));
                    data.Add(row);
                }
            }
            catch
            {
                errors.Add(the_server);
            }
            return data;
        }

        public List<string> getErrors()
        {
            return errors;
        }
    }
}