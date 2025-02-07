using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository repository)
        {
            _bookRepository = repository;
        }

        public void Create(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO is null) throw new EntityNotFoundException($"Book not found");
            if (string.IsNullOrWhiteSpace(bookCreateDTO.Title)) throw new ArgumentNullException("Book title is null or empty");
            Book book = new Book()
            {
                Title = bookCreateDTO.Title,
                Description = bookCreateDTO.Description,
                PublishedYear = bookCreateDTO.PublishedYear
               
            };
           
            _bookRepository.Add(book);
            _bookRepository.Commit();

        }

        public void Delete(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _bookRepository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Book not found");
            _bookRepository.Remove(author);
            _bookRepository.Commit();
        }

        public List<BookGetDTO> GetAll()
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

        public BookGetDTO GetById(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var book = _bookRepository.GetById((int)id);
            if (book is null) throw new EntityNotFoundException("Book not found");
            BookGetDTO bookGetDTO = new BookGetDTO()
            {
                Title = book.Title,
                Id = book.Id,
                Description = book.Description,
                PublishedYear = book.PublishedYear

            };
            return bookGetDTO;

        }

        public void Update(int? id, BookUpdateDTO bookUpdateDTO)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var book = _bookRepository.GetById((int)id);
            if (book is null) throw new EntityNotFoundException("Book not found");
            book.Description = bookUpdateDTO.Description;
            book.PublishedYear = bookUpdateDTO.PublishedYear;
            book.Title = bookUpdateDTO.Title;

            _bookRepository.Commit();
        }
    }
}
