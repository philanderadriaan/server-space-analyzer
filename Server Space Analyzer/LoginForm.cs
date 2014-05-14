using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Server_Space_Analyzer
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {
            InitializeComponent();
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            ok_button.Hide();
            Scanner scanner = new Scanner(new Credential(username_textbox.Text, password_textbox.Text));
            scanner.SomethingHappened += this.HandleEvent;

            List<List<string>> data = new List<List<string>>();
            data.Add(new List<string>(new string[] { "Server", "Volume", "Capacity", "Free Space" }));
            List<string> servers = new RDGReader("nksd_servers.rdg").read("name");
            foreach (string server in servers)
            {
                data = data.Concat(scanner.scan(server)).ToList();
            }
            new XLSWriter("Server Spaces.xlsx").overwrite(data);
            Hide();


            string message = "Operation complete.";
            if (scanner.getErrors().Count() > 0)
            {
                message += "\n\nCannot connect to:";
                foreach (string server in scanner.getErrors())
                {
                    message += "\n" + server;
                }
            }
            MessageBox.Show(message);

            System.Diagnostics.Process.Start(@"Server Spaces.xlsx");
            Close();
        }

        public void HandleEvent(object sender, EventArgs args)
        {
            status_label.Text = "Scanning " + ((Scanner)sender).getServer();
            status_label.TextAlign = ContentAlignment.MiddleCenter;
            
            Console.WriteLine(ok_button.Text);
        }
    }
}