using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookWriter : IDisposable
    {
        private readonly BinaryWriter _writer;

        public BookWriter(BinaryWriter writer)
        {
            _writer = writer;
        }

        public void WriteBook(Book target)
        {
            if (target == null)
            {
                return;
            }

            try
            {
                this._writer.Write(target.Isbn);
                this._writer.Write(target.Author);
                this._writer.Write(target.Title);
                this._writer.Write(target.Publisher);
                this._writer.Write(target.YearPublished);
                this._writer.Write(target.Pages);
                this._writer.Write(target.Price);
            }
            catch (IOException exc)
            {
                throw new IOException("IOException was thrown while writing a Book (BinaryWriter::Write).", exc);
            }
            catch (ObjectDisposedException exc)
            {
                throw new ObjectDisposedException(exc.Message);
            }
        }

        // Because BookWriter is just an adapter we use a short form of Dispose pattern.
        public void Dispose()
        {
            _writer.Dispose();
        }

        public void Close()
        {
            _writer.Close();
        }
    }
}
