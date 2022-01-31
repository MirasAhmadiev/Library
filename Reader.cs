using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_true
{
    class Reader
    {
        public string Name;
        public Book Book;
        public int Term; 
        public int LeftTerm;

        public Reader(string name, Book book, int term)
        {
            Name = name;
            Book = book;
            Term = term;
            LeftTerm = term;
        }
    }
}
