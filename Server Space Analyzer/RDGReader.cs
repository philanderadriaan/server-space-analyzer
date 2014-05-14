using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server_Space_Analyzer
{
    class RDGReader
    {
        private XmlDocument my_document = new XmlDocument();

        public RDGReader(string the_path)
        {
            my_document.Load(the_path);
        }

        public List<string> read(string the_tag)
        {
            List<string> output = new List<string>();
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
