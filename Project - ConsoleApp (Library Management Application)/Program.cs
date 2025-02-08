using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Borrowers;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Loan;
using Project___ConsoleApp__Library_Management_Application_.DTOs.LoanItem;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Helpers;
using Project___ConsoleApp__Library_Management_Application_.Repository.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Service.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Service.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            AuthorRepository authorRepository = new AuthorRepository();
            BookRepository bookRepository = new BookRepository();
            BorrowerRepository borrowerRepository = new BorrowerRepository();
            LoanRepository loanRepository = new LoanRepository();
            LoanItemRepository loanItemRepository = new LoanItemRepository();


            
            IAuthorService authorService = new AuthorService(authorRepository,bookRepository);
            IBookService bookService = new BookService(bookRepository, authorRepository);
            IBorrowerService borrowerService = new BorrowerService(borrowerRepository);
            ILoanService loanService = new LoanService(loanRepository);
            ILoanItemService loanItemService = new LoanItemService(loanItemRepository);

            ConsoleKeyInfo mainMenu;
            do
            {
                Console.Clear();
                Helper.MainMenu();
                mainMenu = Console.ReadKey(intercept: true);

                switch (mainMenu.Key)
                {
                    case ConsoleKey.D1: ManageAuthors(authorService); break;
                    case ConsoleKey.D2: ManageBooks(bookService, authorService); break;
                    case ConsoleKey.D3: ManageBorrowers(borrowerService); break;
                    case ConsoleKey.D4: BorrowBook(borrowerService, loanService, loanItemService, bookService); break;
                    case ConsoleKey.D5: ReturnBook(loanService); break;
                    case ConsoleKey.D6: ShowMostBorrowedBooks(loanItemService); break;
                    case ConsoleKey.D7: ShowLateBorrowers(loanService, borrowerService); break;
                    case ConsoleKey.D8: ShowAllBorrowers(borrowerService); break;
                    case ConsoleKey.D9: ApplyFilters(bookService); break;
                    case ConsoleKey.Escape: Console.Clear(); break;
                    default:
                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                        Pause();
                        break;
                }
            } while (mainMenu.Key != ConsoleKey.Escape);
        }
        static void ManageAuthors(IAuthorService authorService)
        {
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
                            Helper.ChangeTextColor(ConsoleColor.Cyan, $"Author ID: {author.Id} | Name: {author.Name}");
                            Console.WriteLine("Books Written:");

                            if (author.Books.Any())
                            {
                                foreach (var book in author.Books)
                                {
                                    Console.WriteLine($"   - {book.Title} (Published: {book.PublishedYear})");
                                }
                            }
                            else
                            {
                                Helper.ChangeTextColor(ConsoleColor.DarkYellow, "   No books found for this author.");
                            }

                            Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
                        }

                        Pause();
                        break;

                    case ConsoleKey.D2: 
                        Console.Clear();
                        Console.Write("Enter the author's name: ");
                        string name = Console.ReadLine()?.Trim(); 

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Author name cannot be empty!");
                        }
                        else if (name.Length < 2)
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Author name must be at least 2 characters long!");
                        }
                        else
                        {
                            try
                            {
                                var newAuthor = new AuthorCreateDTO { Name = name };
                                authorService.Create(newAuthor);
                                Helper.ChangeTextColor(ConsoleColor.Green, $"Author '{name}' has been successfully added!");
                            }
                            catch (Exception ex) 
                            {
                                Helper.ChangeTextColor(ConsoleColor.Red, $"An error occurred while adding the author: {ex.Message}");
                            }
                        }

                        Pause();
                        break;


                    case ConsoleKey.D3: 
                        Console.Clear();
                        Console.Write("Enter author ID to update: ");

                        if (!int.TryParse(Console.ReadLine(), out int authorId))
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Invalid author ID!");
                        }
                        else
                        {
                            var existingAuthor = authorService.GetById(authorId);

                            if (existingAuthor == null)
                            {
                                Helper.ChangeTextColor(ConsoleColor.Red, $"Author with ID {authorId} not found!");
                            }
                            else
                            {
                                Console.WriteLine("\nCurrent Author Details:");
                                Console.WriteLine($"ID: {existingAuthor.Id}");
                                Console.WriteLine($"Name: {existingAuthor.Name}");

                                Console.Write("\nEnter new name for author (leave empty to keep current name): ");
                                string newName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(newName))
                                {
                                    Helper.ChangeTextColor(ConsoleColor.Yellow, "Update canceled. Author name cannot be empty.");
                                }
                                else
                                {
                                    authorService.Update(authorId, new AuthorUpdateDTO { Name = newName });
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Author ID {authorId} has been successfully updated!");
                                }
                            }
                        }

                        Pause();
                        break;


                    case ConsoleKey.D4: 
                        Console.Clear();
                        Console.Write("Enter author ID to delete: ");

                        if (!int.TryParse(Console.ReadLine(), out int deleteAuthorId))
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Invalid author ID! Please enter a valid numeric value.");
                        }
                        else
                        {
                            var existingAuthor = authorService.GetById(deleteAuthorId);

                            if (existingAuthor == null)
                            {
                                Helper.ChangeTextColor(ConsoleColor.Red, $"Author with ID {deleteAuthorId} not found!");
                            }
                            else
                            {
                                Console.WriteLine("\nAuthor Details:");
                                Console.WriteLine($"ID: {existingAuthor.Id}");
                                Console.WriteLine($"Name: {existingAuthor.Name}");

                                Console.Write("\nAre you sure you want to delete this author? (Y/N): ");
                                ConsoleKey confirmationKey = Console.ReadKey().Key;

                                if (confirmationKey == ConsoleKey.Y)
                                {
                                    authorService.Delete(deleteAuthorId);
                                    Helper.ChangeTextColor(ConsoleColor.Green, $"Author ID {deleteAuthorId} has been successfully deleted!");
                                }
                                else
                                {
                                    Helper.ChangeTextColor(ConsoleColor.Yellow, "Deletion canceled.");
                                }
                            }
                        }

                        Pause();
                        break;




                    case ConsoleKey.Escape: Console.Clear(); break;
                    default:
                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                        Pause();
                        break;
                }
            } while (authorMenu.Key != ConsoleKey.Escape);
        }
        static void ManageBooks(IBookService bookService, IAuthorService authorService)
        {
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

                        if (allBooks == null || !allBooks.Any())
                        {
                            Helper.ChangeTextColor(ConsoleColor.Yellow, "No books found in the system.");
                        }
                        else
                        {
                            foreach (var book in allBooks)
                            {
                                Console.WriteLine($"ID: {book.Id} | Title: {book.Title} | Year: {book.PublishedYear}");
                                Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
                            }
                        }

                        Pause();
                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        while (true)
                        {
                            try
                            {
                                Console.Clear();
                                Console.Write("Enter new book title: ");
                                var title = Console.ReadLine()?.Trim();
                                if (string.IsNullOrWhiteSpace(title))
                                    throw new ArgumentException("Book title cannot be empty or null.");

                                Console.Write("Enter new book description: ");
                                var description = Console.ReadLine()?.Trim();
                                if (string.IsNullOrWhiteSpace(description))
                                    throw new ArgumentException("Book description cannot be empty or null.");

                                Console.Write("Enter new book published year: ");
                                if (!int.TryParse(Console.ReadLine(), out int publishedYear) || publishedYear < 500 || publishedYear > 2025)
                                    throw new ArgumentException("Published year must be between 500 and 2025.");

                                Console.Write("\nDo you want to assign an author to this book? (Y/N): ");
                                var assignAuthor = Console.ReadLine()?.Trim().ToLower();

                                List<int> authorIds = new List<int>();

                                if (assignAuthor == "y")
                                {
                                    var authors = authorService.GetAll();
                                    if (authors == null || !authors.Any())
                                        throw new Exception("No authors found in the system. Please add authors first.");

                                    Console.WriteLine("\nAvailable Authors:");
                                    foreach (var author in authors)
                                        Console.WriteLine($"{author.Id} - {author.Name}");

                                    Console.Write("\nEnter the author ID for this book: ");
                                    int authorId;
                                    while (!int.TryParse(Console.ReadLine()?.Trim(), out authorId) || !authors.Any(a => a.Id == authorId))
                                    {
                                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid author ID. Please enter a valid ID from the list.");
                                        Console.Write("\nEnter the author ID for this book: ");
                                    }

                                    authorIds.Add(authorId);
                                }

                                var bookDto = new BookCreateDTO
                                {
                                    Title = title,
                                    Description = description,
                                    PublishedYear = publishedYear,
                                    AuthorIds = authorIds.Any() ? authorIds : null  // Optional author
                                };

                                bookService.Create(bookDto);
                                Helper.ChangeTextColor(ConsoleColor.Green, "Book added successfully.");
                                break;
                            }
                            catch (ArgumentException ex)
                            {
                                Helper.ChangeTextColor(ConsoleColor.Red, $"Error: {ex.Message}");
                            }
                            catch (Exception ex)
                            {
                                Helper.ChangeTextColor(ConsoleColor.Red, $"An unexpected error occurred: {ex.Message}");
                            }
                        }
                        Pause();
                        break;

                    case ConsoleKey.D3: 
                        Console.Clear();
                        Console.Write("Enter book ID to update: ");

                        if (!int.TryParse(Console.ReadLine(), out int bookId))
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Invalid book ID!");
                            Pause();
                            break;
                        }

                        var existingBook = bookService.GetById(bookId);
                        if (existingBook == null)
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, $"Book with ID {bookId} not found!");
                            Pause();
                            break;
                        }

                        Console.WriteLine("\nCurrent Book Details:");
                        Console.WriteLine($"ID: {existingBook.Id}");
                        Console.WriteLine($"Title: {existingBook.Title}");
                        Console.WriteLine($"Description: {existingBook.Description}");
                        Console.WriteLine($"Year: {existingBook.PublishedYear}");

                        Console.Write("\nEnter new title (leave empty to keep current): ");
                        string bookTitle = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(bookTitle)) bookTitle = existingBook.Title;

                        Console.Write("Enter new description (leave empty to keep current): ");
                        string bookDescription = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(bookDescription)) bookDescription = existingBook.Description;

                        Console.Write("Enter new published year (leave empty to keep current): ");
                        string yearInput = Console.ReadLine()?.Trim();
                        int bookYear = existingBook.PublishedYear;
                        if (!string.IsNullOrWhiteSpace(yearInput) && (!int.TryParse(yearInput, out bookYear) || bookYear < 1000 || bookYear > DateTime.Now.Year))
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Invalid year entered. Update canceled.");
                            Pause();
                            break;
                        }

                        try
                        {
                            bookService.Update(bookId, new BookUpdateDTO
                            {
                                Title = bookTitle,
                                Description = bookDescription,
                                PublishedYear = bookYear
                            });

                            Helper.ChangeTextColor(ConsoleColor.Green, $"Book ID {bookId} has been successfully updated!");
                        }
                        catch (Exception ex)
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, $"An error occurred while updating the book: {ex.Message}");
                        }

                        Pause();
                        break;

                    case ConsoleKey.D4: 
                        Console.Clear();
                        Console.Write("Enter book ID to delete: ");

                        if (!int.TryParse(Console.ReadLine(), out int deleteBookId))
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Invalid book ID!");
                            Pause();
                            break;
                        }

                        var bookToDelete = bookService.GetById(deleteBookId);
                        if (bookToDelete == null)
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, $"Book with ID {deleteBookId} not found!");
                            Pause();
                            break;
                        }

                        Console.WriteLine("\nBook Details:");
                        Console.WriteLine($"ID: {bookToDelete.Id}");
                        Console.WriteLine($"Title: {bookToDelete.Title}");

                        Console.Write("\nAre you sure you want to delete this book? (Y/N): ");
                        ConsoleKey confirmationKey = Console.ReadKey().Key;

                        if (confirmationKey == ConsoleKey.Y)
                        {
                            try
                            {
                                bookService.Delete(deleteBookId);
                                Helper.ChangeTextColor(ConsoleColor.Green, $"Book ID {deleteBookId} has been successfully deleted!");
                            }
                            catch (Exception ex)
                            {
                                Helper.ChangeTextColor(ConsoleColor.Red, $"An error occurred while deleting the book: {ex.Message}");
                            }
                        }
                        else
                        {
                            Helper.ChangeTextColor(ConsoleColor.Yellow, "Deletion canceled.");
                        }

                        Pause();
                        break;


                    case ConsoleKey.Escape: Console.Clear(); break;
                    default:
                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                        Pause();
                        break;
                }
            } while (bookMenu.Key != ConsoleKey.Escape);
        }
        static void ManageBorrowers(IBorrowerService borrowerService)
        {
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
                        foreach (var borrower in allBorrowers)
                        {
                            Console.WriteLine($"ID: {borrower.Id} | Name: {borrower.Name} | Email: {borrower.Email}");
                            Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
                        }
                        Pause();
                        break;

                    case ConsoleKey.D2: 
                        Console.Clear();
                        Console.Write("Enter borrower name: ");
                        string borrowerName = Console.ReadLine();
                        Console.Write("Enter borrower email: ");
                        string email = Console.ReadLine();

                        borrowerService.Create(new BorrowerCreateDTO
                        {
                            Name = borrowerName,
                            Email = email
                        });

                        Helper.ChangeTextColor(ConsoleColor.Green, $"Borrower {borrowerName} has been successfully added!");
                        Pause();
                        break;

                    case ConsoleKey.Escape: Console.Clear(); break;
                    default:
                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation. Please select a valid option.");
                        Pause();
                        break;
                }
            } while (borrowerMenu.Key != ConsoleKey.Escape);
        }
        static void BorrowBook(IBorrowerService borrowerService, ILoanService loanService, ILoanItemService loanItemService, IBookService bookService)
        {
            Console.Clear();
            Console.WriteLine("Available Books:");
            var books = bookService.GetAll();
            var loanItems = loanItemService.GetAll();
            var borrowedBookIds = loanItems.Where(li => loanService.GetById(li.LoanId).ReturnDate == null)
                                           .Select(li => li.BookId)
                                           .ToHashSet();

            var availableBooks = books.Where(b => !borrowedBookIds.Contains(b.Id)).ToList();
            foreach (var book in availableBooks)
            {
                Console.WriteLine($"ID: {book.Id} | Title: {book.Title}");
            }

            Console.Write("Enter the ID of the book to borrow: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId) || availableBooks.All(b => b.Id != bookId))
            {
                Console.WriteLine("Invalid book selection.");
                return;
            }

            Console.Write("Enter Borrower ID: ");
            if (!int.TryParse(Console.ReadLine(), out int borrowerId))
            {
                Console.WriteLine("Invalid Borrower ID.");
                return;
            }

            var loanCreateDto = new LoanCreateDTO
            {
                BorrowerId = borrowerId,
                LoanDate = DateTime.Now,
                MustReturnDate = DateTime.Now.AddDays(15)
            };

            loanService.Create(loanCreateDto);

            var loanItemCreateDto = new LoanItemCreateDTO
            {
                LoanId = loanCreateDto.BorrowerId,
                BookId = bookId
            };
            loanItemService.Create(loanItemCreateDto);

            Console.WriteLine("Book borrowed successfully!");
        }
        static void ReturnBook(ILoanService loanService)
        {
            Console.Clear();
            Console.Write("Enter Borrower ID: ");

            if (!int.TryParse(Console.ReadLine(), out int borrowerId) || borrowerId < 1)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Invalid Borrower ID.");
                Pause();
                return;
            }

            
            var loan = loanService.GetAll().FirstOrDefault(l => l.BorrowerId == borrowerId && l.ReturnDate == null);

            if (loan == null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "No active loans found for this borrower.");
                Pause();
                return;
            }

            Console.WriteLine($"Loan found: Loan ID {loan.BorrowerId}, Must Return Date: {loan.MustReturnDate}");

            
            Console.Write("\nConfirm book return? (Y/N): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                loan.ReturnDate = DateTime.UtcNow;
                loanService.Update(loan.BorrowerId, new LoanUpdateDTO
                {
                    LoanDate = loan.LoanDate,
                    MustReturnDate = loan.MustReturnDate,
                    ReturnDate = loan.ReturnDate
                });

                Helper.ChangeTextColor(ConsoleColor.Green, "Book returned successfully.");
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Yellow, "Operation canceled.");
            }

            Pause();
        }
        static void ShowMostBorrowedBooks(ILoanItemService loanItemService)
        {
            Console.Clear();
            var bookCounts = loanItemService.GetAll()
                .GroupBy(li => li.BookId)
                .Select(group => new { BookId = group.Key, Count = group.Count() })
                .OrderByDescending(b => b.Count)
                .FirstOrDefault();

            if (bookCounts == null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "No books have been borrowed yet.");
                Pause();
                return;
            }

            Console.WriteLine($"Most borrowed book ID: {bookCounts.BookId} | Times borrowed: {bookCounts.Count}");
            Pause();
        }
        static void ShowLateBorrowers(ILoanService loanService, IBorrowerService borrowerService)
        {
            Console.Clear();
            DateTime today = DateTime.UtcNow;

            var overdueLoans = loanService.GetAll()
                .Where(l => l.MustReturnDate < today && l.ReturnDate == null)
                .ToList();

            if (!overdueLoans.Any())
            {
                Helper.ChangeTextColor(ConsoleColor.Green, "No overdue borrowers.");
                Pause();
                return;
            }

            Console.WriteLine("Late Borrowers:");
            foreach (var loan in overdueLoans)
            {
                var borrower = borrowerService.GetById(loan.BorrowerId);
                Console.WriteLine($"Borrower ID: {borrower.Id}, Name: {borrower.Name}, Email: {borrower.Email}, Must Return Date: {loan.MustReturnDate}");
            }

            Pause();
        }

       
        static void ShowAllBorrowers(IBorrowerService borrowerService)
        {
            Console.Clear();
            var allBorrowers = borrowerService.GetAll();
            foreach (var borrower in allBorrowers)
            {
                Console.WriteLine($"ID: {borrower.Id} | Name: {borrower.Name} | Email: {borrower.Email}");
                Helper.ChangeTextColor(ConsoleColor.Yellow, "--------------------------------------");
            }
            Pause();
        }



        static void ApplyFilters(IBookService bookService)
        {
            ConsoleKeyInfo filterMenu;
            do
            {
                Console.Clear();
                Helper.FilterMenu();
                filterMenu = Console.ReadKey(intercept: true);

                switch (filterMenu.Key)
                {
                    case ConsoleKey.D1: 
                        Console.Clear();
                        Console.Write("Enter book title to filter: ");
                        string title = Console.ReadLine();
                        var filteredBooks = bookService.GetAll().Where(b => b.Title == title).ToList();

                        if (filteredBooks.Any())
                        {
                            filteredBooks.ForEach(book => Console.WriteLine($"ID: {book.Id} | Title: {book.Title}"));
                        }
                        else
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "No books found with the given title.");
                        }

                        Pause();
                        break;

                    case ConsoleKey.D2: 
                        Console.Clear();
                        Console.Write("Enter author name to filter: ");
                        string authorName = Console.ReadLine();
                        var booksByAuthor = bookService.GetAll()
                            .Where(b => b.Authors.Any(a => a.Name == authorName))
                            .ToList();

                        if (booksByAuthor.Any())
                        {
                            booksByAuthor.ForEach(book => Console.WriteLine($"ID: {book.Id} | Title: {book.Title}"));
                        }
                        else
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "No books found for the given author.");
                        }

                        Pause();
                        break;

                    case ConsoleKey.Escape: Console.Clear(); break;
                    default:
                        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid operation.");
                        Pause();
                        break;
                }
            } while (filterMenu.Key != ConsoleKey.Escape);
        }


        static void Pause()
        {
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}
