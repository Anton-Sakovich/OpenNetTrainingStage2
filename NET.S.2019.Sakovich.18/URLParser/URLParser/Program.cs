using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

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

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static int Main(string[] args)
        {
            // The first command line argument is the path to the input file with
            // urls. The second command line argument is the path to the output file.
            // So, first of all, check that there are enough arguments.
            if (args.Length < 2)
            {
                Console.WriteLine("Two arguments are required.");
                return NOT_ENOUGH_ARGUMENTS;
            }

            // If there are at least two command line arguments, then assign the first one
            // to input file name and the second one to output file name.
            string inputFileName = args[0];
            string outputFileName = args[1];

            // Now, when we have a path to the input file, try to open it for reading.
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(inputFileName);
            }
            catch (Exception exc)
            {
                // If we cannot even open the file, then terminate the program.
                Console.WriteLine("Cannot open {0}", inputFileName);
                Console.WriteLine(exc);
                return INPUT_FILE_OPEN_FAILURE;
            }

            // Now we know that the file name provided is a valid file name and we can configure
            // NLog to use a logfile.

            // We have waited till here before configuring NLog because we want to use a log file
            // with the name which is the same as the name of the input file but with extension .log.
            string logFileName = Path.ChangeExtension(inputFileName, ".log");

            // Delete a possible log from the previous program run.
            File.Delete(logFileName);

            // Configure NLog.
            ConfigureNLog(logFileName);

            // Log message: we have managed to open the file!.
            logger.Info("Opened {0} for reading data.", inputFileName);

            // Create a parser.
            URLParser parser = new URLParser(line => URLData.Parse(line), data => data.ToXElement());

            // Log message: we have created a parser!
            logger.Info("Initialized {0} as the parser.", parser);

            // Try to parse the file.
            // xroot is the (root) <urladdresses> XElement.
            XElement xroot = null;
            using (reader)
            {
                try
                {
                    xroot = parser.ParseFile(reader);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Cannot read from {0}", inputFileName);
                    Console.WriteLine(exc.Message);
                    logger.Fatal(exc, "Cannot read from {0}", inputFileName);
                    return INPUT_FILE_READ_FAILURE;
                }
            }

            // Log message: we have parsed the file!
            logger.Info("Parsed to XEelement.");

            // Try to open the output file.
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(outputFileName);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Cannot open {0}", outputFileName);
                Console.WriteLine(exc);
                logger.Fatal(exc, "Cannot open {0}", outputFileName);
                return OUTPUT_FILE_OPEN_FAILURE;
            }

            // Log message: we have opened the output file!
            logger.Info("Openned {0} for writing data.", outputFileName);

            // Write a string representation of xroot to the output file.
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
                    Console.WriteLine("Cannot write to {0}", outputFileName);
                    Console.WriteLine(exc.Message);
                    logger.Fatal(exc, "Cannot write to {0}", outputFileName);
                    return OUTPUT_FILE_WRITE_FAILURE;
                }
            }

            // Log message: we have written to the output file!
            logger.Info("Wrote data to {0}.", args[1]);

            // Make the program wait until NLog finishes up.
            LogManager.Shutdown();

            Console.WriteLine("Done.");

            return 0;
        }

        private static void ConfigureNLog(string fname)
        {
            LoggingConfiguration config = new LoggingConfiguration();

            FileTarget logfile = new FileTarget("logfile")
            {
                FileName = fname
            };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            LogManager.Configuration = config;
        }
    }
}
