using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public interface IBookTagSelector<T>
    {
        T SelectTag(Book book);
    }
}
