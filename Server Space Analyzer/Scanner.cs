using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Server_Space_Analyzer
{
    public class Scanner
    {
        private Credential my_credential;
        private List<String> blank = new List<String>();
        private Formatter my_formatter = new Formatter();

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
                ManagementScope scope = new ManagementScope(name_space, options);
                ObjectQuery query = new ObjectQuery("select VolumeName, Name, Size, Freespace from Win32_LogicalDisk where DriveType=3");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection collection = searcher.Get();
                Console.Out.WriteLine();
                Console.Out.WriteLine(the_server);
                Boolean is_first = true;
                data.Add(blank);
                foreach (ManagementObject i in collection)
                {
                    String volume = i["VolumeName"].ToString();
                    String name = i["Name"].ToString();
                    String total = i["Size"].ToString();
                    String free = i["FreeSpace"].ToString();
                    String formatted_name = my_formatter.formatName(volume, name);
                    String formatted_total = my_formatter.formatCapacity(total);
                    String formatted_free = my_formatter.formatCapacity(free);
                    List<String> row = new List<String>();
                    if (is_first)
                    {
                        row.Add(the_server);
                        is_first = false;
                    }
                    else
                    {
                        row.Add("");
                    }
                    row.Add(formatted_name);
                    row.Add(formatted_total);
                    row.Add(formatted_free);
                    data.Add(row);
                    String output = my_formatter.format(volume, name, total, free);
                    Console.Out.WriteLine(output);
                }
            }
            catch
            {
            }
            return data;
        }
    }
}