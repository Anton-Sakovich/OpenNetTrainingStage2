﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksTask
{
    public class BookList
    {
        private IList<Book> books;

        public BookList(IEnumerable<Book> booksSource)
        {
            books = booksSource.ToList();
        }

        private BookList(List<Book> booksSource)
        {
            books = booksSource;
        }

        public static BookList LoadFromFile(string fname)
        {
            List<Book> booksSoure = new List<Book>();

            using (FileStream fstream = new FileStream(fname, FileMode.Open, FileAccess.Read))
            {
                using (BookReader reader = new BookReader(new BinaryReader(fstream)))
                {
                    while (fstream.Position < fstream.Length)
                    {
                        booksSoure.Add(reader.ReadBook());
                    }
                }
            }

            return new BookList(booksSoure);
        }

        public event EventHandler<BookChangedEventArgs> AddingBook;

        public event EventHandler<BookChangedEventArgs> BookAdded;

        public event EventHandler<BookChangedEventArgs> RemovingBook;

        public event EventHandler<BookChangedEventArgs> BookRemoved;

        private void OnAddingBook(Book addBook)
        {
            AddingBook?.Invoke(this, new BookChangedEventArgs(addBook));
        }

        private void OnBookAdded(Book addBook)
        {
            BookAdded?.Invoke(this, new BookChangedEventArgs(addBook));
        }

        private void OnRemovingBook(Book removeBook)
        {
            RemovingBook?.Invoke(this, new BookChangedEventArgs(removeBook));
        }

        private void OnBookRemoved(Book removeBook)
        {
            BookRemoved?.Invoke(this, new BookChangedEventArgs(removeBook));
        }

        public IReadOnlyList<Book> Books
        {
            get
            {
                return (IReadOnlyList<Book>)books;
            }
        }

        public void SaveToFile(string fname)
        {
            using (BookWriter writer = new BookWriter(new BinaryWriter(new FileStream(fname, FileMode.Create))))
            {
                foreach (Book book in books)
                {
                    writer.WriteBook(book);
                }
            }
        }

        public void AddBook(Book newBook)
        {
            if (books.Contains(newBook))
            {
                throw new AddDuplicateBookException();
            }

            OnAddingBook(newBook);

            books.Add(newBook);

            OnBookAdded(newBook);
        }

        public void RemoveBook(Book removeBook)
        {
            if (!books.Contains(removeBook))
            {
                throw new BookNotFoundException();
            }

            OnRemovingBook(removeBook);

            books.Remove(removeBook);

            OnBookRemoved(removeBook);
        }

        public void SortBooksByTag<T>(IBookTag<T> tag)
            where T : IComparable<T>
        {
            // Insertion sort
            Book temp;

            for (int outer = 1; outer < this.books.Count; outer++)
            {
                for (int inner = outer; inner > 0; inner--)
                {
                    if (tag.GetTag(this.books[inner - 1]).CompareTo(tag.GetTag(this.books[inner])) > 0)
                    {
                        temp = this.books[inner - 1];
                        this.books[inner - 1] = this.books[inner];
                        this.books[inner] = temp;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public Book FindBookByTag<T>(IBookTag<T> tag, T value)
            where T : IEquatable<T>
        {
            foreach (Book book in this.books)
            {
                if (tag.GetTag(book).Equals(value))
                {
                    return book;
                }
            }

            throw new BookNotFoundException();
        }
    }
}
