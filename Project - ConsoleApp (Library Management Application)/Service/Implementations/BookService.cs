using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public void Create(BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO is null) throw new EntityNotFoundException($"Book not found");
            if (string.IsNullOrWhiteSpace(bookCreateDTO.Title)) throw new ArgumentNullException("Book title is null or empty");
            Book book = new Book();
            book.Title = bookCreateDTO.Title;   
            book.Description = bookCreateDTO.Description;
            book.PublishedYear = bookCreateDTO.PublishedYear;

            _repository.Add(book);
            _repository.Commit();

        }

        public void Delete(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _repository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Author not found");
            _repository.Remove(author);
            _repository.Commit();
        }

        public List<BookGetDTO> GetAll()
        {
            List<BookGetDTO> mappedAuthors = new List<BookGetDTO>();
            List<Book> books = _repository.GetAll();
            foreach (var book in books)
            {
                BookGetDTO bookGetDTO = new BookGetDTO();
                bookGetDTO.Title = book.Title;
                bookGetDTO.Id = book.Id;
                bookGetDTO.PublishedYear = book.PublishedYear;
                mappedAuthors.Add(bookGetDTO);
            }

            return mappedAuthors;
        }

        public BookGetDTO GetById(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var book = _repository.GetById((int)id);
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
            var book = _repository.GetById((int)id);
            if (book is null) throw new EntityNotFoundException("Book not found");
            book.Description = bookUpdateDTO.Description;
            book.PublishedYear = bookUpdateDTO.PublishedYear;
            book.Title = bookUpdateDTO.Title;

            _repository.Commit();
        }
    }
}
