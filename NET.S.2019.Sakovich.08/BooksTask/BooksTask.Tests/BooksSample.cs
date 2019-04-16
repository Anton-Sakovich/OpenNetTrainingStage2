using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask.Tests
{
    static class BooksSample
    {
        public static readonly Book LifeIsStrangeBook = new Book("123", "Max Caulfield", "Life is Strange", "Blackwell Academy", 2013, 202, 47);

        public static Book[] Books = new Book[]
        {
            new Book("123", "Author1", "Title1", "Publisher1", 1995, 202, 102),
            new Book("256", "Author2", "Title2", "Publisher2", 2003, 304, 95)
        };

        public static Book[] BooksSortedByPrice = new Book[]
        {
            new Book("256", "Author2", "Title2", "Publisher2", 2003, 304, 95),
            new Book("123", "Author1", "Title1", "Publisher1", 1995, 202, 102)
        };

        public static byte[] BooksBytes = new byte[]
        {
            3, 49, 50, 51, // "123" (BLOB)
            7, 65, 117, 116, 104, 111, 114, 49, // "Authro1" (BLOB)
            6, 84, 105, 116, 108, 101, 49, // "Title1" (BLOB)
            10, 80, 117, 98, 108, 105, 115, 104, 101, 114, 49, // Publisher1 (BLOB)
            203, 7, 0, 0, // 0x 00 00 07 CB (1995) in Little Endian
            202, 0, 0, 0, // 0x 00 00 00 CA (202) in Little Endian
            102, 0, 0, 0, // 0x 00 00 00 66 (102) in Little Endian
            3, 50, 53, 54,
            7, 65, 117, 116, 104, 111, 114, 50,
            6, 84, 105, 116, 108, 101, 50,
            10, 80, 117, 98, 108, 105, 115, 104, 101, 114, 50,
            211, 7, 0, 0,
            48, 1, 0, 0,
            95, 0, 0, 0
        };
    }
}
