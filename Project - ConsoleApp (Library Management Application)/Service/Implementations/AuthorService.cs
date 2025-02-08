using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public void Create(AuthorCreateDTO authorCreateDTO)
        {
            if (authorCreateDTO == null)
                throw new ArgumentNullException(nameof(authorCreateDTO), "Author data is null.");
            if (string.IsNullOrWhiteSpace(authorCreateDTO.Name))
                throw new ArgumentException("Author name cannot be null or empty.");

            Author author = new Author { Name = authorCreateDTO.Name };

            _authorRepository.Add(author);
            _authorRepository.Commit();

            Console.WriteLine($"Success: Author '{author.Name}' has been created.");
        }

        public void Delete(int? id)
        {
            if (id == null || id < 1)
                throw new InvalidIdException("Invalid author ID.");

            var author = _authorRepository.GetById((int)id);
            if (author == null)
                throw new EntityNotFoundException("Author not found.");

            _authorRepository.Remove(author);
            _authorRepository.Commit();

            Console.WriteLine($"Success: Author with ID {id} has been deleted.");
        }

        public List<AuthorGetDTO> GetAll()
        {
            return _authorRepository.GetAllAsQuery()
                .Include(a => a.Books)
                .Select(author => new AuthorGetDTO
                {
                    Id = author.Id,
                    Name = author.Name,
                    Books = author.Books.Select(book => new BookGetAuthorDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        PublishedYear = book.PublishedYear
                    }).ToList()
                })
                .ToList();
        }

        public AuthorGetDTO GetById(int? id)
        {
            if (id == null || id < 1)
                throw new InvalidIdException("Invalid author ID.");

            var author = _authorRepository.GetAllAsQuery()
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
                throw new EntityNotFoundException("Author not found.");

            return new AuthorGetDTO
            {
                Id = author.Id,
                Name = author.Name,
                Books = author.Books.Select(book => new BookGetAuthorDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    PublishedYear = book.PublishedYear
                }).ToList()
            };
        }

        public void Update(int? id, AuthorUpdateDTO authorUpdateDTO)
        {
            if (id == null || id < 1)
                throw new InvalidIdException("Invalid author ID.");
            if (authorUpdateDTO == null)
                throw new ArgumentNullException(nameof(authorUpdateDTO), "Author update data is null.");
            if (string.IsNullOrWhiteSpace(authorUpdateDTO.Name))
                throw new ArgumentException("Author name cannot be null or empty.");

            var author = _authorRepository.GetById((int)id);
            if (author == null)
                throw new EntityNotFoundException("Author not found.");

            author.Name = authorUpdateDTO.Name;
            _authorRepository.Commit();

            Console.WriteLine($"Success: Author ID {id} has been updated.");
        }

        public void AssignBooksToAuthor(int authorId, List<int> bookIds)
        {
            if (authorId < 1)
                throw new ArgumentOutOfRangeException(nameof(authorId), "Invalid author ID.");
            if (bookIds == null || !bookIds.Any())
                throw new ArgumentException("Book IDs list cannot be null or empty.");

            var author = _authorRepository.GetAllAsQuery()
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == authorId);

            if (author == null)
                throw new EntityNotFoundException("Author not found.");

            var books = _bookRepository.GetAllAsQuery().Where(b => bookIds.Contains(b.Id)).ToList();
            if (!books.Any())
                throw new EntityNotFoundException("No valid books found to assign.");

            author.Books = books;
            _authorRepository.Commit();

            Console.WriteLine($"Success: Assigned {books.Count} books to author '{author.Name}'.");
        }

       
        public List<AuthorGetDTO> FilterAuthorsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Author name cannot be null or empty.");

            return _authorRepository.GetAllAsQuery()
                .Where(a => EF.Functions.Like(a.Name, $"%{name}%"))
                .Include(a => a.Books)
                .Select(author => new AuthorGetDTO
                {
                    Id = author.Id,
                    Name = author.Name,
                    Books = author.Books.Select(book => new BookGetAuthorDTO
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Description = book.Description,
                        PublishedYear = book.PublishedYear
                    }).ToList()
                })
                .ToList();
        }
    }
}
