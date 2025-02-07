using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Borrowers;
using Project___ConsoleApp__Library_Management_Application_.Helpers;
using Project___ConsoleApp__Library_Management_Application_.Repository.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Service.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            AuthorRepository authorRepository = new AuthorRepository();
            IAuthorService authorService = new AuthorService(authorRepository);

            BookRepository bookRepository = new BookRepository();
            IBookService bookService = new BookService(bookRepository);

            BorrowerRepository borrowerRepository = new BorrowerRepository();
            IBorrowerService borrowerService = new BorrowerService(borrowerRepository);
           
            ConsoleKeyInfo mainMenu;

            do
            {
                Console.Clear();
                Helper.MainMenu();

                mainMenu = Console.ReadKey(intercept: true);

                switch (mainMenu.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        ConsoleKeyInfo authorMenu;


                        do
                        {
                            Console.Clear();
                            Helper.AuthorMenu();
                            authorMenu = Console.ReadKey(intercept: true);
                            switch (authorMenu.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    
                                    var allAuthors = authorService.GetAll();
                                    foreach (var author in allAuthors)
                                    {
                                        Console.WriteLine(author);
                                        Console.WriteLine("Books:\n");
                                        foreach (var book in author.Books)
                                        {
                                            Console.WriteLine(book);
                                            Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
                                           
                                        }
                                    }
                                    Pause();
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();
                                    Console.WriteLine("Enter the name of the author: ");
                                    string name = Console.ReadLine();
                                    AuthorCreateDTO authorCreateDTO = new AuthorCreateDTO()
                                    {
                                        Name = name
                                    };

                                    authorService.Create(authorCreateDTO);
                                    
                                    Pause();
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();

                                    Console.WriteLine("Enter the id of the author to update: ");
                                    int authorUpdateId = int.Parse(Console.ReadLine());

                                    Console.WriteLine("Enter new name for the author: ");
                                    string authorUpdateName = Console.ReadLine();

                                    var authorUpdate = authorService.GetById(authorUpdateId);

                                    AuthorUpdateDTO authorUpdateDTO = new AuthorUpdateDTO()
                                    {
                                        Name = authorUpdateName
                                    };

                                    var authorPreviousName = authorUpdate.Name; 
                                    authorUpdate.Name = authorUpdateName;
                                    authorService.Update(authorUpdateId, authorUpdateDTO);
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"{authorPreviousName} has been successfully changed to {authorUpdateName}");
                                    Pause();
                                    break;
                                case ConsoleKey.D4:
                                    Console.Clear();

                                    Console.WriteLine("Enter Id of the author to delete: ");
                                    int authorDeleteId = int.Parse(Console.ReadLine());
                                    authorService.Delete(authorDeleteId);

                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Author {authorDeleteId} has been successfully deleted!");
                                    Pause();
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Clear();
                                    break;

                                default:
                                    Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                                    Pause();
                                    break;
                            }
                           
                            } while (authorMenu.Key != ConsoleKey.Escape);
                        break;
                       

                    case ConsoleKey.D2:
                        Console.Clear();
                        
                        ConsoleKeyInfo bookMenu;


                        do
                        {
                            Console.Clear();
                            Helper.BookMenu();
                            bookMenu = Console.ReadKey(intercept: true);
                            switch (bookMenu.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    var allBooks = bookService.GetAll();
                                    foreach (var book in allBooks)
                                    {
                                        Console.WriteLine(book);
                                        Console.WriteLine("Authors:\n");
                                        foreach (var author in book.Authors)
                                        {
                                            Console.WriteLine(author);
                                            Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
                                        }
                                      
                                    }
                                    Pause();
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();

                                    Console.WriteLine("Enter the title of the book: ");
                                    string title = Console.ReadLine();

                                    Console.WriteLine("Enter the description of the book: ");
                                    string description = Console.ReadLine();

                                    Console.WriteLine("Enter the published year of the book: ");
                                    int publishedYear = int.Parse(Console.ReadLine());

                                    BookCreateDTO bookCreateDTO = new BookCreateDTO()
                                    {
                                        Title = title,
                                        Description = description,
                                        PublishedYear = publishedYear
                                    };

                                    bookService.Create(bookCreateDTO);
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"{bookCreateDTO.Title} has been successfully created!");
                                    Pause();
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();
                                    

                                    Console.WriteLine("Enter the id of the book to update: ");
                                    int bookUpdateId = int.Parse(Console.ReadLine());
                                    Console.WriteLine(bookService.GetById(bookUpdateId));

                                    Console.WriteLine("Enter new Title of the book: ");
                                    string bookUpdateTitle = Console.ReadLine();

                                    Console.WriteLine("Enter new Description of the book: ");
                                    string bookUpdateDescription = Console.ReadLine();

                                    Console.WriteLine("Enter the Published year of the book: ");
                                    int bookUpdatePublishedYear = int.Parse(Console.ReadLine());

                                    BookUpdateDTO bookUpdateDTO = new BookUpdateDTO()
                                    {
                                        Title = bookUpdateTitle,
                                        Description = bookUpdateDescription,
                                        PublishedYear = bookUpdatePublishedYear
                                    };

                                    bookService.Update(bookUpdateId, bookUpdateDTO);
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Book {bookUpdateId} successfully updated!");
                                    Pause();
                                    break;
                                case ConsoleKey.D4:
                                    Console.Clear();
                                    

                                    Console.WriteLine("Enter the id of the book to delete: ");
                                    int bookDeleteId = int.Parse(Console.ReadLine());

                                    bookService.Delete(bookDeleteId);
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Book {bookDeleteId} successfully deleted!");

                                    Pause();
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Clear();
                                    break;

                                default:
                                    Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                                    Pause();
                                    break;
                            }

                        } while (bookMenu.Key != ConsoleKey.Escape);
                        
                        break;

                    case ConsoleKey.D3:
                        Console.Clear();

                        ConsoleKeyInfo borrowerMenu;


                        do
                        {
                            Console.Clear();
                            Helper.BorrowerMenu();
                            borrowerMenu = Console.ReadKey(intercept: true);
                            switch (borrowerMenu.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    

                                    var allBorrowers = borrowerService.GetAll();
                                    Console.WriteLine("Loans:\n");

                                    foreach (var borrower in allBorrowers)
                                    {
                                        Console.WriteLine(borrower);
                                        foreach (var loan in borrower.Loans)
                                        {
                                            Console.WriteLine(loan);
                                            Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
                                        }

                                    }

                                    Pause();
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();

                                    Console.WriteLine("Enter the borrower name: ");
                                    string borrowerName = Console.ReadLine();

                                    Console.WriteLine("Enter the borrower email: ");
                                    string email = Console.ReadLine();  

                                    BorrowerCreateDTO borrowerCreateDTO = new BorrowerCreateDTO()
                                    {
                                        Name = borrowerName,
                                        Email = email
                                    };

                                    borrowerService.Create(borrowerCreateDTO);
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Borrower {borrowerName} successfully created!");

                                    Pause();
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();
                                    

                                    Console.WriteLine("Enter the id of the borrower to update: ");
                                    int borrowerUpdateId = int.Parse(Console.ReadLine());
                                    Console.WriteLine(borrowerService.GetById(borrowerUpdateId));

                                    Console.WriteLine("Enter the name of the borrower to update: ");
                                    string borrowerUpdateName = Console.ReadLine();

                                    Console.WriteLine("Enter the email of the borrower to update: ");
                                    string borrowerUpdateEmail = Console.ReadLine();

                                    BorrowerUpdateDTO borrowerUpdateDTO = new BorrowerUpdateDTO()
                                    {
                                        Name = borrowerUpdateName,
                                        Email = borrowerUpdateEmail
                                    };

                                    borrowerService.Update(borrowerUpdateId, borrowerUpdateDTO);

                                    Helper.ChangeTextColor(ConsoleColor.Green, $"{borrowerUpdateName} has been successfully updated!");

                                    Pause();
                                    break;
                                case ConsoleKey.D4:
                                    Console.Clear();

                                    Console.WriteLine("Enter the id of the borrower to delete: ");
                                    int borrowerDeleteId = int.Parse(Console.ReadLine());

                                    borrowerService.Delete(borrowerDeleteId);

                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Borrower {borrowerDeleteId} has been successfully deleted!");


                                    Pause();
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Clear();
                                    break;

                                default:
                                    Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                                    Pause();
                                    break;
                            }

                        } while (borrowerMenu.Key != ConsoleKey.Escape);
                        Pause();
                        break;

                    case ConsoleKey.D4:
                        Console.Clear();

                        Pause();
                        break;

                    case ConsoleKey.D5:
                        Console.Clear();

                        Pause();
                        break;

                    case ConsoleKey.D6:
                        Console.Clear();

                        Pause();
                        break;

                    case ConsoleKey.D7:
                        Console.Clear();

                        Pause();
                        break;

                    case ConsoleKey.D8:
                        Console.Clear();

                        Pause();
                        break;

                    case ConsoleKey.D9:
                        Console.Clear();

                        ConsoleKeyInfo filterMenu;

                        do
                        {
                            Console.Clear() ;
                            Helper.FilterMenu();

                            filterMenu = Console.ReadKey(intercept: true);
                            switch (filterMenu.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    Console.WriteLine("Enter the title of book to filter: ");
                                    string title = Console.ReadLine();
                                    FilterBooksByTitle(title);

                                    Pause();
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Clear();
                                    break;

                                default:
                                    Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                                    Pause();
                                    break;
                            }

                        } while (true);

                        Pause();
                        break;

                    case ConsoleKey.Escape:
                        Console.Clear();
                        break;

                    default:
                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                        Pause();
                        break;
                }
            } while (mainMenu.Key != ConsoleKey.Escape);
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }

        static void FilterBooksByTitle(string title)
        {
            BookRepository bookRepository = new BookRepository();
            IBookService bookService = new BookService(bookRepository);

            var allBooks = bookService.GetAll();
            var foundBooks = allBooks.FindAll(x => x.Title == title);

            foundBooks.ForEach(x => Console.WriteLine(x));

        }
        static void FilterBooksByAuthor(string authorName)
        {
            BookRepository bookRepository = new BookRepository();
            IBookService bookService = new BookService(bookRepository);

            var allAuthors = bookService.GetAll();
            
            allAuthors.ForEach(x => Console.WriteLine(x));

        }
    }
}


