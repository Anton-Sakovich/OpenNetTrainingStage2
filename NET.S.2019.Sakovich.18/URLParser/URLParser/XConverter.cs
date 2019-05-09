using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace URLParser
{
    public static class XConverter
    {
        public static XElement ToXElement(this URLData urlData)
        {
            XElement xroot = new XElement("urlAdress", new XElement("host", new XAttribute("name", urlData.Host)));

            if (urlData.PathSegments.Length > 0)
            {
                XElement xuri = new XElement("uri");

                foreach (string pathSegment in urlData.PathSegments)
                {
                    xuri.Add(new XElement("segment", pathSegment));
                }

                xroot.Add(xuri);
            }

            if (urlData.Parameters.Count > 0)
            {
                XElement xparams = new XElement("parameters");

                foreach (KeyValuePair<string, string> pair in urlData.Parameters)
                {
                    xparams.Add(new XElement("parameter", new XAttribute("key", pair.Key), new XAttribute("value", pair.Value)));
                }

                xroot.Add(xparams);
            }

            return xroot;
        }
    }
}
