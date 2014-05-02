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
        Credential my_credential;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            String username = username_textbox.Text;
            String password = password_textbox.Text;
            my_credential = new Credential(username, password);

            ok_button.Enabled = false;
            ok_button.Text = "Scanning...";
            XmlDocument document = new XmlDocument();
            document.Load("nksd_servers.rdg");
            XmlNodeList servers = document.GetElementsByTagName("name");
            Credential credential = new Credential(username, password);
            Scanner crawler = new Scanner(credential);
            List<List<String>> data = new List<List<String>>();
            List<String> header = new List<String>();
            header.Add("Server");
            header.Add("Volume");
            header.Add("Capacity");
            header.Add("Free Space");
            data.Add(header);
            foreach (XmlElement i in servers)
            {
                String server = i.InnerText;
                List<List<String>> server_data = crawler.scan(server);
                data = data.Concat(server_data).ToList();
            }
            ExcelWriter writer = new ExcelWriter();
            writer.overwrite("Server Spaces.xlsx", data);

        }

        public Credential getCredential()
        {
            return my_credential;
        }
    }
}