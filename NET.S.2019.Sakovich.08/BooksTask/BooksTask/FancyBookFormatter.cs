using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class FancyBookFormatter : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        public string Format(string format, object arg, IFormatProvider provider)
        {
            if ((arg is Book book) && (format == "Fancy"))
            {
                return $"Book(Title = \"{book.Title}\", Author = \"{book.Author}\"";
            }

            return Fallback(format, arg, provider);
        }

        private string Fallback(string format, object arg, IFormatProvider provider)
        {
            if (arg is IFormattable formattable)
            {
                return formattable.ToString(format, provider);
            }
            else if (arg != null)
            {
                return arg.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
