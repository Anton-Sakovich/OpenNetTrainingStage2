using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BooksTask
{
    public class BookWriter : IDisposable
    {
        readonly BinaryWriter _Writer;

        public BookWriter(BinaryWriter writer)
        {
            _Writer = writer;
        }

        public void WriteBook(Book target)
        {
            if(target == null)
            {
                return;
            }

            try
            {
                this._Writer.Write(target.Isbn);
                this._Writer.Write(target.Author);
                this._Writer.Write(target.Title);
                this._Writer.Write(target.Publisher);
                this._Writer.Write(target.YearPublished);
                this._Writer.Write(target.Pages);
                this._Writer.Write(target.Price);
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
            _Writer.Dispose();
        }

        public void Close()
        {
            _Writer.Close();
        }
    }
}
