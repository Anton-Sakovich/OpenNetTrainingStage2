using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{

    public interface IBookTag<T>
    {
        T GetTag(Book book);
    }
}
