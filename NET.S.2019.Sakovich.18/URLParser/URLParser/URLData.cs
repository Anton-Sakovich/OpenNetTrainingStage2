using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace URLParser
{
    public class URLData
    {
        public static readonly Regex SchemeRegex = new Regex(@"[a-zA-Z][a-zA-Z0-9\+\.\-]+", RegexOptions.Compiled);
        
        // Taken from https://stackoverflow.com/a/3824105
        public static readonly Regex HostRegex = new Regex(@"([a-zA-Z0-9]|([a-zA-Z0-9][a-zA-Z0-9\-]{0,61}[a-zA-Z0-9]))(\.([a-zA-Z0-9]|([a-zA-Z0-9][a-zA-Z0-9\-]{0,61}[a-zA-Z0-9])))*", RegexOptions.Compiled);

        public static readonly Regex PathRegex = new Regex(@"(/[a-zA-Z0-9\-\._~%]*)*", RegexOptions.Compiled);

        public static readonly Regex QueryRegex = new Regex(@"\?([^=&]+=[^=&]+)(&[^=&]+=[^=&]+)*", RegexOptions.Compiled);

        public static readonly Regex URLRegex = new Regex("^" + SchemeRegex.ToString() + "://" + HostRegex.ToString() + PathRegex.ToString() + QueryRegex.ToString() + "$", RegexOptions.Compiled);

        private static readonly string[] splitters = new string[] { "://", "/", "?" };

        public static URLData Parse(string url)
        {
            if (!URLRegex.IsMatch(url))
            {
                return null;
            }

            string[] tokens = url.Split(splitters, StringSplitOptions.None);

            string scheme = tokens[0];

            string host = tokens[1];

            int pathSegmentsCount;

            Dictionary<string, string> parameters = null;

            if (url.Contains("?"))
            {
                pathSegmentsCount = tokens.Length - 3;
                parameters = tokens.Last().Split('&').Select(pair => pair.Split('=')).ToDictionary(pair => pair[0], pair => pair[1]);
            }
            else
            {
                pathSegmentsCount = tokens.Length - 2;
                parameters = new Dictionary<string, string>(0);
            }

            string[] pathSegments = new string[pathSegmentsCount];

            for (int i = 0; i < pathSegmentsCount; i++)
            {
                pathSegments[i] = tokens[i + 2];
            }

            return new URLData()
            {
                Scheme = scheme,
                Host = host,
                PathSegments = pathSegments,
                Parameters = parameters
            };
        }

        public string Scheme { get; set; }

        public string Host { get; set; }

        public string[] PathSegments { get; set; }

        public Dictionary<string,string> Parameters { get; set; }

        private URLData()
        {
        }
    }
}
