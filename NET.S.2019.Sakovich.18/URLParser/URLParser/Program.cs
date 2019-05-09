using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace URLParser
{
    internal class Program
    {
        public const int NOT_ENOUGH_ARGUMENTS = 1;

        public const int INPUT_FILE_OPEN_FAILURE = 2;

        public const int OUTPUT_FILE_OPEN_FAILURE = 3;

        public const int INPUT_FILE_READ_FAILURE = 4;

        public const int OUTPUT_FILE_WRITE_FAILURE = 5;

        public const int GENERAL_ERROR = 127;

        public static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Two arguments are required.");
                return NOT_ENOUGH_ARGUMENTS;
            }

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(args[0]);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Cannot open " + args[0]);
                Console.WriteLine(exc);
                return INPUT_FILE_OPEN_FAILURE;
            }

            URLParser parser = new URLParser(line => URLData.Parse(line), data => data.ToXElement());

            XElement xroot = null;
            using (reader)
            {
                try
                {
                    xroot = parser.ParseFile(reader);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Cannot read from " + args[0]);
                    Console.WriteLine(exc.Message);
                    return INPUT_FILE_READ_FAILURE;
                }
            }

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(args[1]);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Cannot open " + args[1]);
                Console.WriteLine(exc);
                return OUTPUT_FILE_OPEN_FAILURE;
            }

            using (writer)
            {
                try
                {
                    foreach (string outline in xroot.ToString().Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        writer.WriteLine(outline);
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Cannot write " + args[1]);
                    Console.WriteLine(exc.Message);
                    return OUTPUT_FILE_WRITE_FAILURE;
                }
            }

            Console.WriteLine("Done.");
            return 0;
        }
    }
}
