using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_true
{
    class Library
    {
        public string LibraryName;
        public List<Book> ListOfBooks = new List<Book>()
        {
                {new Book("Chryseis Knight", "The Great Big Lion") },
                {new Book("Kunal Basu", "In An Ideal World") },
                {new Book("Savita Chhabra", "Legacy of Learning") },
                {new Book("Barack Obama", "A Promised Land") },
                {new Book("Dr. Pradeep Kumar Srivastava", "Bye Bye Corona") },               

        };
        public List<Reader> Ledger = new List<Reader>();

        public Library(string libraryName)
        {
            LibraryName = libraryName;
        }

        public List<Book> SearchBook(string name)
        {
            
            List<Book> newList = new List<Book>();

            foreach (var book in ListOfBooks)
            {
                if (book.Title.ToLower().Contains(name.ToLower()))
                {
                    newList.Add(book);
                }
            }
            return newList;
        }

        public void RemoveReader(int IndexOfReaderToRemove)
        {
            Ledger.RemoveAt(IndexOfReaderToRemove);
        }

        public List<Book> SortByAuthor()
        {
            
            return ListOfBooks.OrderBy(book => book.Author).ToList();
        }

        public List<Book> SortByTitle()
        {
            
            return ListOfBooks.OrderBy(book => book.Title).ToList();
        }

        public void AddBook(string author, string bookTitle)
        {
            
            ListOfBooks.Add(new Book(author, bookTitle));
        }

        public void RemoveBook(string author, string bookTitle)
        {
            
            int i = 0;
            int elementIdToRemoveFromLedger = 0;
            bool toRemove = false;
            foreach (var book in ListOfBooks)
            {
                if (book.Author == author && book.Title == bookTitle)
                {
                    elementIdToRemoveFromLedger = i;
                    toRemove = true;
                    break;
                }
                i++;
            }
            if (toRemove == true)
            {
                if (ListOfBooks[elementIdToRemoveFromLedger].IsAvailable)
                {
                    ListOfBooks.RemoveAt(elementIdToRemoveFromLedger);
                    Console.WriteLine("Book is removed.");
                }
                else
                {
                    Console.WriteLine("Someone is reading this book. Wait tiil it return to library!");
                }
            }
            else
            {
                Console.WriteLine("Nothing found, nothing removed.");
            }
        }
    }
}
