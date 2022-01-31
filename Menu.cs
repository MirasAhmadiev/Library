using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_true
{
    class Menu
    {
        static public void Start(Library library)
        {
            int day = 1;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Day: {day}. Welcome to {library.LibraryName} Library! \n");
                Console.WriteLine("'Reading improves the quality of your life'");
                
                switch (AskMainMenuPoint())
                {
                    case 1:
                        TheListOfBooks(library);
                        break;
                    case 2:
                        SearchBook(library);
                        break;
                    case 3:
                        SortByAuthor(library);
                        break;
                    case 4:
                        SortByTitle(library);
                        break;
                    case 5:
                        AddBook(library);
                        break;
                    case 6:
                        RemoveBook(library);
                        break;
                    case 7:
                        TakeBook(library);
                        break;
                    case 8:
                        ShowStatistic(library);
                        break;
                    case 9:
                        Environment.Exit(0);
                        break;
                }
                CheckAvaliablityCounters(library);
                day++;
            }
        }

        static public void CheckAvaliablityCounters(Library library)
        {
            
            int readerNumber = 0;
            if (library.Ledger.Count > 0)
            {
                for (int i = 0; i < library.Ledger.Count; i++)
                {
                    if (library.Ledger[i].LeftTerm == 0)
                    {
                        int getBookIndex = SearchBookIndexByNameAndAuthor(library, library.Ledger[i].Book.Title, library.Ledger[i].Book.Author) - 1;
                        if (getBookIndex >= 0 && getBookIndex < library.ListOfBooks.Count)
                        {
                            library.ListOfBooks[getBookIndex].IsAvailable = true;
                        }
                        library.RemoveReader(i);
                        i--;
                    }
                    else
                    {
                        library.Ledger[i].LeftTerm--;
                    }
                    readerNumber++;
                }
            }
            else
            {
                foreach (var book in library.ListOfBooks)
                {
                    book.IsAvailable = true;
                }
            }
        }

        static public void TakeBook(Library library)
        {
            string ReaderName;
            int bookIndexUserChosen;
            int term;
           
            if (TheListOfAvaliableBooks(library) > 0)
            {
                
                bookIndexUserChosen = ChooseBook(library) - 1;
                
                int BookIndexInGlobalList = GetBookIndexInGlobalIndex(library, bookIndexUserChosen);
                
                term = AskTermToReadBook();
                
                ReaderName = ChooseName(library);
                
                while (!CheckNameIfExist(library, ReaderName))
                {
                    Console.WriteLine("\n This reader is already reading some books. You are not allowed to get more!\n");
                    ReaderName = ChooseName(library);
                }
                
                library.Ledger.Add(new Reader(ReaderName, library.ListOfBooks[BookIndexInGlobalList], term));
                
                library.ListOfBooks[bookIndexUserChosen].IsAvailable = false;
            }
            else
            {
                Console.WriteLine("\n No books avaliable right now, try your luck tomorrow :)");
            }
        }

        static public int GetBookIndexInGlobalIndex(Library library, int bookIndexUserChosen)
        {
            int indx = 0;
            int kindx = 0;
            foreach (var book in library.ListOfBooks)
            {
                if (book.IsAvailable == true)
                {
                    indx++;
                }
                if (bookIndexUserChosen == indx)
                {
                    return kindx;
                }
                kindx++;
            }
            return bookIndexUserChosen;
        }

        static public int AskTermToReadBook()
        {
            Console.WriteLine("\n How long would you like to read this book? [1-14 days] Select and press \"Enter\":");
            int termOutput;
            bool isNum = int.TryParse(Console.ReadLine(), out termOutput);

            while (!isNum || (termOutput < 1 || termOutput > 14))
            {
                Console.WriteLine("\n Wrong inout! Try again[1-14 days] Select and press \"Enter\":");
                isNum = int.TryParse(Console.ReadLine(), out termOutput);
            }
            return termOutput;
        }

        static public bool CheckNameIfExist(Library library, string name)
        {
            foreach (var reader in library.Ledger)
            {
                if (name == reader.Name)
                {
                    return false;
                }
            }
            return true;
        }

        static public int TheListOfAvaliableBooks(Library library)
        {
            int indx = 1;
            Console.WriteLine("\n All avaliable books in library:\n");
            foreach (var book in library.ListOfBooks)
            {
                if (book.IsAvailable == true)
                {
                    Console.WriteLine(indx + "." + book.Author + "-" + book.Title);
                    indx++;
                }
            }
            return indx;
        }

        static public string ChooseName(Library library)
        {
            bool check = false;
            Console.WriteLine("\n Enter name [2+ length, latin chars only] and press \"Enter\":");
            string name = Console.ReadLine();
            name = name.Trim(new char[] { ' ' });
            for (int i = 0; i < name.Length; i++)
            {
                if (name.ToLower()[i] >= 'a' && name[i] <= 'z' && name[i] != ' ')
                {
                    check = true;
                }
                else
                {
                    check = false;
                    break;
                }
            }
            while (name.Length < 2 || !check)
            {
                Console.WriteLine("\n Wrong input! Enter again and press \"Enter\":");
                Console.Beep(330, 500);
                name = Console.ReadLine();
                name = name.Trim(new char[] { ' ' });
                for (int i = 0; i < name.Length; i++)
                {
                    if (name.ToLower()[i] >= 'a' && name[i] <= 'z' && name[i] != ' ')
                    {
                        check = true;
                    }
                    else
                    {
                        check = false;
                        break;
                    }
                }
            }
            return name;
        }

        static public int ChooseBook(Library library)
        {
           
            List<Book> avaliableBooksList = new List<Book>();
            foreach (var book in library.ListOfBooks)
            {
                if (book.IsAvailable == true)
                {
                    avaliableBooksList.Add(book);
                }
            }
           
            Console.WriteLine($"\n Enter number of book [1-5] and press \"Enter\":");
            string input = Console.ReadLine();
            int.TryParse(input, out int numInput);
            while (!int.TryParse(input, out numInput) || numInput > avaliableBooksList.Count || numInput < 1)
            {
                Console.WriteLine("\n Bad input! Try again. Enter number [1-5] and press \"Enter\":");
                input = Console.ReadLine();
            }
            int globalIndex = SearchBookIndexByNameAndAuthor(library, avaliableBooksList[numInput - 1].Title, avaliableBooksList[numInput - 1].Author);
            return globalIndex;
        }

        static public int SearchBookIndexByNameAndAuthor(Library library, string name, string author)
        {
            int indx = 0;
            foreach (var book in library.ListOfBooks)
            {
                if (book.Title == name && book.Author == author)
                {
                    return indx + 1;
                }
                indx++;
            }
            return 0;
        }

        static public int AskMainMenuPoint()
        {
            Console.WriteLine($"\n Please select an option by number [1-9] and press \"Enter\": \n"
                + "1 - The list of books in our library \n"
                + "2 - Search book \n"
                + "3 - Sort books by author \n"
                + "4 - Sort books by title \n"
                + "5 - Add book \n"
                + "6 - Remove book \n"
                + "7 - Take book \n"
                + "8 - Show book statistic\n"
                + "9 - Exit");

            int menuPoint;
            bool isNum = int.TryParse(Console.ReadLine(), out menuPoint);

            while (!isNum || (menuPoint < 1 || menuPoint > 9))
            {
                Console.WriteLine("\n Wrong inout! Try again. Select options by number [1-9] and press \"Enter\":");
                isNum = int.TryParse(Console.ReadLine(), out menuPoint);
            }
            return menuPoint;
        }

        static public void TheListOfBooks(Library library)
        {
            Console.WriteLine("\n All books in library:\n");
            foreach (var book in library.ListOfBooks)
            {
                Console.WriteLine(book.Author + " - " + book.Title + " [" + book.IsAvailable + "]");
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey(true);
        }

        static public void SearchBook(Library library)
        {
            string name = " ";
            while (name == " ")
            {
                Console.Write("\n Enter book title to search and press \"Enter\":");
                name = Console.ReadLine().Trim();
                foreach (char x in name)
                {
                    if (!char.IsLetter(x))
                    {
                        name = " ";
                        Console.WriteLine("\n Wrong title. Try again.");
                        break;
                    }
                }
                if (name.Length == 0)
                {
                    name = " ";
                    Console.WriteLine("\n Title is empty.");
                    continue;
                }
            }


            List<Book> newList = library.SearchBook(name);
            if (newList.Count > 0)
            {
                Console.WriteLine("\n Found such books:");
                foreach (var book in newList)
                {
                    Console.WriteLine(book.Author + " - " + book.Title + " [" + book.IsAvailable + "]");
                }
            }
            else
            {
                Console.WriteLine("\n Nothing found!");
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey(true);
        }

        static public void SortByAuthor(Library library)
        {
            Console.WriteLine("\n All books in library sorted by Author:\n");
            foreach (var book in library.SortByAuthor())
            {
                Console.WriteLine(book.Author + " - " + book.Title + " [" + book.IsAvailable + "]");
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey(true);
        }

        static public void SortByTitle(Library library)
        {
            Console.WriteLine("\n All books in library sorted by Title:\n");
            foreach (var book in library.SortByTitle())
            {
                Console.WriteLine(book.Author + " - " + book.Title + " [" + book.IsAvailable + "]");
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey(true);
        }

        static public void AddBook(Library library)
        {
            string author = " ";
            while (author == " ")
            {
                Console.Write("\n Enter book author to add and press \"Enter\":");
                author = Console.ReadLine().Trim();
                foreach (char x in author)
                {
                    if (!char.IsLetter(x))
                    {
                        author = " ";
                        Console.WriteLine("\n Wrong author. Try again.");
                        break;
                    }
                }
                if (author.Length == 0)
                {
                    author = " ";
                    Console.WriteLine("\n Author is empty.");
                    continue;
                }
            }
            string title = " ";
            while (title == " ")
            {
                Console.Write("\n Enter book title to add and press \"Enter\":");
                title = Console.ReadLine().Trim();
                foreach (char x in title)
                {
                    if (!char.IsLetter(x))
                    {
                        title = " ";
                        Console.WriteLine("\n Wrong title. Try again.");
                        break;
                    }
                }
                if (title.Length == 0)
                {
                    title = " ";
                    Console.WriteLine("\n Title is empty.");
                    continue;
                }
            }
            library.AddBook(author, title);
            TheListOfBooks(library);
        }

        static public void RemoveBook(Library library)
        {
            string author = " ";
            while (author == " ")
            {
                Console.Write("\n Enter book author to remove and press \"Enter\":");
                author = Console.ReadLine().Trim();
                foreach (char x in author)
                {
                    if (!char.IsLetter(x))
                    {
                        author = " ";
                        Console.WriteLine("\n Wrong author. Try again.");
                        break;
                    }
                }
                if (author.Length == 0)
                {
                    author = " ";
                    Console.WriteLine("\n Title is empty.");
                    continue;
                }
            }
            string title = " ";
            while (title == " ")
            {
                Console.Write("\n Enter book title to remove and press \"Enter\":");
                title = Console.ReadLine().Trim();
                foreach (char x in title)
                {
                    if (!char.IsLetter(x))
                    {
                        title = " ";
                        Console.WriteLine("\n Wrong title. Try again.");
                        break;
                    }
                }
                if (title.Length == 0)
                {
                    title = " ";
                    Console.WriteLine("\n Title is empty.");
                    continue;
                }
            }
            library.RemoveBook(author, title);
            TheListOfBooks(library);
        }

        static public void ShowStatistic(Library library)
        {
            int avaliable = 0;
            foreach (var book in library.ListOfBooks)
            {
                if (book.IsAvailable == true) { avaliable++; }
            }
            Console.WriteLine($"\n There are {library.ListOfBooks.Count} books in total: {avaliable} avaliable and {library.ListOfBooks.Count - avaliable} in use.\n");
            if (library.Ledger.Count > 0)
            {
                int index = 1;
                foreach (var reader in library.Ledger)
                {
                    Console.WriteLine($"{index}. User {reader.Name} got book \"{reader.Book.Title}\" wrote by \"{reader.Book.Author}\" for {reader.Term} days and {reader.LeftTerm} days more left!");
                    index++;
                }
            }
            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
