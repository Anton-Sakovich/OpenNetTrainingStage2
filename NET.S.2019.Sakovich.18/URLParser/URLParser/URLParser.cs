using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NLog;

namespace URLParser
{
    public class URLParser
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly Func<string, URLData> parser;
        private readonly Func<URLData, XElement> xbuilder;

        public URLParser(Func<string, URLData> parser, Func<URLData, XElement> xbuilder)
        {
            this.parser = parser;
            this.xbuilder = xbuilder;
        }

        public XElement ParseFile(StreamReader reader)
        {
            XElement xroot = new XElement("urlAddresses");

            string line = reader.ReadLine();

            while (line != null)
            {
                URLData url = this.parser(line);

                if (url != null)
                {
                    xroot.Add(xbuilder(url));
                }
                else
                {
                    logger.Warn("Failed to parse \"{0}\" (skipped it).", line);
                }

                line = reader.ReadLine();
            }

            return xroot;
        }
    }
}
