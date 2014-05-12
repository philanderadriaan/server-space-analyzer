using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Space_Analyzer
{
    public class Scanner
    {
        private Credential my_credential;
        private Formatter my_formatter = new Formatter();
        private List<String> errors = new List<String>();

        public Scanner(Credential the_credential)
        {
            my_credential = the_credential;
        }

        public List<List<String>> scan(String the_server)
        {
            List<List<String>> data = new List<List<String>>();
            try
            {
                ConnectionOptions options = new ConnectionOptions();
                options.Username = my_credential.getUsername();
                options.Password = my_credential.getPassword();
                String name_space = @"\\";
                if (the_server != "")
                {
                    name_space += the_server;
                }
                else
                {
                    name_space += ".";
                }
                name_space += @"\root\cimv2";
                ManagementObjectCollection collection = new ManagementObjectSearcher(new ManagementScope(name_space, options), new ObjectQuery("select VolumeName, Name, Size, Freespace from Win32_LogicalDisk where DriveType=3")).Get();
                bool first = true;
                data.Add(new List<String>());
                foreach (ManagementObject i in collection)
                {
                    List<String> row = new List<String>();
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
                if (the_server.Equals(the_server.ToUpper()))
                {
                    MessageBox.Show("Can't connect to " + the_server);
                }
            }
            return data;
        }
    }
}