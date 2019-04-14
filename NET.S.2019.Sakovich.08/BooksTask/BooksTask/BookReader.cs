using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookReader : IDisposable
    {
        private BinaryReader _reader;

        public BookReader(BinaryReader reader)
        {
            this._reader = reader;
        }

        public Book ReadBook()
        {
            Book newBook = null;

            try
            {
                string isbn = _reader.ReadString();
                string author = _reader.ReadString();
                string title = _reader.ReadString();
                string publisher = _reader.ReadString();
                uint yearsPublished = _reader.ReadUInt32();
                uint pages = _reader.ReadUInt32();
                uint price = _reader.ReadUInt32();

                newBook = new Book(isbn, author, title, publisher, yearsPublished, pages, price);
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

            return newBook;
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public void Close()
        {
            _reader.Close();
        }
    }
}
