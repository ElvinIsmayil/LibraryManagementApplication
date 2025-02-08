using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;
using Project___ConsoleApp__Library_Management_Application_.Service.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public void Create(BookCreateDTO bookCreateDTO)
        {
            try
            {
                if (bookCreateDTO == null || string.IsNullOrWhiteSpace(bookCreateDTO.Title) || string.IsNullOrWhiteSpace(bookCreateDTO.Description))
                    throw new ArgumentException("Book title or description cannot be null or empty.");

                var book = new Book
                {
                    Title = bookCreateDTO.Title,
                    Description = bookCreateDTO.Description,
                    PublishedYear = bookCreateDTO.PublishedYear,
                    Authors = new List<Author>() // Empty author list by default
                };

                if (bookCreateDTO.AuthorIds != null && bookCreateDTO.AuthorIds.Any())
                {
                    var authors = _authorRepository.GetAllAsQuery()
                        .Where(a => bookCreateDTO.AuthorIds.Contains(a.Id))
                        .ToList();

                    if (authors.Any())
                        book.Authors.AddRange(authors);
                }

                _bookRepository.Add(book);
                _bookRepository.Commit();

                Console.WriteLine($"Success: Book '{book.Title}' added successfully.");
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }







        public void Delete(int? id)
        {
            try
            {
                if (id == null || id < 1) throw new InvalidIdException("Invalid book ID.");

                var book = _bookRepository.GetById((int)id);
                if (book == null) throw new EntityNotFoundException("Book not found.");

                _bookRepository.Remove(book);
                _bookRepository.Commit();

                Console.WriteLine($"Success: Book with ID {id} has been deleted.");
            }
            catch (InvalidIdException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }

        public List<BookGetDTO> GetAll()
        {
            try
            {
                return _bookRepository.GetAllAsQuery()
                    .Include(x => x.Authors)
                    .Select(book => new BookGetDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        PublishedYear = book.PublishedYear,
                        Authors = book.Authors.Select(author => new AuthorGetBookDTO
                        {
                            Id = author.Id,
                            Name = author.Name
                        }).ToList()
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error while fetching books: {ex.Message}");
                return new List<BookGetDTO>(); // Return an empty list instead of failing
            }
        }

        public BookGetDTO GetById(int? id)
        {
            try
            {
                if (id == null || id < 1) throw new InvalidIdException("Invalid book ID.");

                var book = _bookRepository.GetById((int)id);
                if (book == null) throw new EntityNotFoundException("Book not found.");

                return new BookGetDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedYear = book.PublishedYear,
                    Authors = book.Authors.Select(author => new AuthorGetBookDTO
                    {
                        Id = author.Id,
                        Name = author.Name
                    }).ToList()
                };
            }
            catch (InvalidIdException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return null;
            }
        }

        public void Update(int? id, BookUpdateDTO bookUpdateDTO)
        {
            try
            {
                if (id == null || id < 1) throw new ArgumentOutOfRangeException("Invalid book ID.");
                if (bookUpdateDTO == null) throw new ArgumentNullException(nameof(bookUpdateDTO));
                if (string.IsNullOrWhiteSpace(bookUpdateDTO.Title))
                    throw new ArgumentException("Book title cannot be empty.");

                var book = _bookRepository.GetById((int)id);
                if (book == null) throw new EntityNotFoundException("Book not found.");

                book.Title = bookUpdateDTO.Title;
                book.Description = bookUpdateDTO.Description;
                book.PublishedYear = bookUpdateDTO.PublishedYear;

                _bookRepository.Commit();

                Console.WriteLine($"Success: Book ID {id} has been updated.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
        }
        public List<BookGetDTO> FilterBooksByAuthor(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
                throw new ArgumentException("Author name cannot be null or empty.");

            return _bookRepository.GetAllAsQuery()
                .Include(b => b.Authors)
                .Where(b => b.Authors.Any(a => a.Name.Contains(authorName)))
                .Select(book => new BookGetDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedYear = book.PublishedYear
                })
                .ToList();
        }
        public List<BookGetDTO> FilterBooksByTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Book title cannot be null or empty.");

            return _bookRepository.GetAllAsQuery()
                .Where(b => b.Title.Contains(title))
                .Select(book => new BookGetDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedYear = book.PublishedYear
                })
                .ToList();
        }


    }
}
