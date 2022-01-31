using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_true
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library("'Book Ocean'");
            Menu.Start(library);
        }
    }
}
