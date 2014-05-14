using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server_Space_Analyzer
{
    public class RDGReader
    {
        private XmlDocument my_document = new XmlDocument();

        public RDGReader(String the_path)
        {
            my_document.Load(the_path);
        }

        public List<String> read(String the_tag)
        {
            List<String> output = new List<String>();
            XmlNodeList nodes = my_document.GetElementsByTagName(the_tag);
            foreach (XmlElement element in nodes)
            {
                if (element.InnerText.ToUpper().Equals(element.InnerText))
                {
                    output.Add(element.InnerText);
                }
            }
            return output;
        }
    }
}
