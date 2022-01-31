using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_true
{
    class Book
    {
        public string Author;
        public string Title;
        public bool IsAvailable;

        public Book(string author, string title)
        {
            Author = author;
            Title = title;
            IsAvailable = true; 
        }
    }
}
