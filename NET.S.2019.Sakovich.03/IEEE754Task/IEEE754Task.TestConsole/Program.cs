using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE754Task.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a double:");
            double d = double.Parse(Console.ReadLine());

            switch(d)
            {
                case 42:
                    d = double.Epsilon;
                    break;
                case 666:
                    d = double.MinValue;
                    break;
                case 71:
                    d = double.MaxValue;
                    break;
                case 66:
                    d = 0D * (-1D);
                    break;
            }

            Console.WriteLine(d.ToIEEE754String());
            Console.ReadKey();
        }
    }
}
