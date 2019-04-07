using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BooksTask
{
    public class BookReader : IDisposable
    {
        BinaryReader _Reader;

        public BookReader(BinaryReader reader)
        {
            this._Reader = reader;
        }

        public Book ReadBook()
        {
            Book NewBook = null;

            try
            {
                string Isbn = _Reader.ReadString();
                string Author = _Reader.ReadString();
                string Title = _Reader.ReadString();
                string Publisher = _Reader.ReadString();
                uint YearsPublished = _Reader.ReadUInt32();
                uint Pages = _Reader.ReadUInt32();
                uint Price = _Reader.ReadUInt32();

                NewBook = new Book(Isbn, Author, Title, Publisher, YearsPublished, Pages, Price);
            }
            catch (ObjectDisposedException exc)
            {
                throw new IOException("ObjectDisposedException on reading from BinaryReader.", exc);
            }
            catch (EndOfStreamException exc)
            {
                throw new EndOfStreamException("EndOfStreamException on reading from BinaryReader.", exc);
            }
            catch (IOException exc)
            {
                throw new IOException("IOException on reading from BinaryReader.", exc);
            }

            return NewBook;
        }

        public void Dispose()
        {
            _Reader.Dispose();
        }

        public void Close()
        {
            _Reader.Close();
        }
    }
}
