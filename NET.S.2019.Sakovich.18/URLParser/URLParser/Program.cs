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

            string logfname = Path.ChangeExtension(args[0], ".log");
            File.Delete(logfname);
            ConfigureNLog(Path.ChangeExtension(args[0], ".log"));

            logger.Info("Opened {0} for reading data.", args[0]);

            URLParser parser = new URLParser(line => URLData.Parse(line), data => data.ToXElement());

            logger.Info("Initialized {0} as parser.", parser);

            XElement xroot = null;
            using (reader)
            {
                try
                {
                    xroot = parser.ParseFile(reader);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Cannot read from {0}", args[0]);
                    Console.WriteLine(exc.Message);
                    logger.Fatal(exc, "Cannot read from {0}", args[0]);
                    return INPUT_FILE_READ_FAILURE;
                }
            }

            logger.Info("Parsed to XEelement.");

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

            logger.Info("Openned {0} for writing data.", args[1]);

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
                    Console.WriteLine("Cannot write to {0}", args[1]);
                    Console.WriteLine(exc.Message);
                    logger.Fatal("Cannot write to {0}", args[1]);
                    return OUTPUT_FILE_WRITE_FAILURE;
                }
            }

            logger.Info("Wrote data to {0}.", args[1]);

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
