using Project___ConsoleApp__Library_Management_Application_.Helpers;
using Project___ConsoleApp__Library_Management_Application_.Repository.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Service.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    internal class Program
    {
        static void Main(string[] args)
        {

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
                                    AuthorRepository authorRepository = new AuthorRepository();
                                    IAuthorService authorService = new AuthorService(authorRepository);
                                    var allAuthors = authorService.GetAll();
                                    foreach (var item in allAuthors)
                                    {
                                        
                                        foreach (var book in item.BookGetAuthorDTO)
                                        {
                                            Console.WriteLine(book.Title);
                                        }
                                    }
                                    Pause();
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();
                                    
                                   
                                    Pause();
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();
                                    
                                    Pause();
                                    break;
                                case ConsoleKey.D4:
                                    Console.Clear();
                                   
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
                                    
                                    Pause();
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();
                                    Pause();
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();
                                    Pause();
                                    break;
                                case ConsoleKey.D4:
                                    Console.Clear();
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
                                    Pause();
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();
                                    Pause();
                                    break;
                                case ConsoleKey.D3:
                                    Console.Clear();
                                    Pause();
                                    break;
                                case ConsoleKey.D4:
                                    Console.Clear();
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

    }
}


