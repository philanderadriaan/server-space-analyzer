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
            ok_button.Enabled = false;
            ok_button.Text = "Scanning...";
            Scanner scanner = new Scanner(new Credential(username_textbox.Text, password_textbox.Text));
            List<List<String>> data = new List<List<String>>();
            data.Add(new List<String>(new String[] { "Server", "Volume", "Capacity", "Free Space" }));
            List<String> servers = new RDGReader("nksd_servers.rdg").read("name");
            foreach (String server in servers)
            {
                data = data.Concat(scanner.scan(server)).ToList();
            }
            new XLSWriter("Server Spaces.xlsx").overwrite(data);
            Hide();


            String message = "Operation complete.";
            if (scanner.getErrors().Count() > 0)
            {
                message += "\n\nCannot connect to:";
                foreach (String server in scanner.getErrors())
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

        }
    }
}