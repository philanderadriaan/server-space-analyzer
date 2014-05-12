﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server_Space_Analyzer
{
    public class XMLReader
    {
        private XmlDocument my_document;

        public XMLReader(String the_path)
        {
            my_document = new XmlDocument();
            my_document.Load(the_path);
        }

        public List<String> read(String the_tag)
        {
            List<String> output = new List<String>();
            XmlNodeList nodes = my_document.GetElementsByTagName(the_tag);
            foreach (XmlElement element in nodes)
            {
                String text = element.InnerText;
                output.Add(text);
            }
            return output;
        }
    }
}