using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Implementations;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository repository)
        {
            _authorRepository = repository;
        }

        public void Create(AuthorCreateDTO authorCreateDTO)
        {
            try
            {
                if (authorCreateDTO is null) throw new EntityNotFoundException($"Author not found");
                if (string.IsNullOrWhiteSpace(authorCreateDTO.Name)) throw new ArgumentNullException("Author name is null or empty");
                Author author = new Author();
                author.Name = authorCreateDTO.Name;

                _authorRepository.Add(author);
                _authorRepository.Commit();
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            


        }

        public void Delete(int? id)
        {
            if (id is null|| id<1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _authorRepository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Author not found");
            _authorRepository.Remove(author);
            _authorRepository.Commit();
        }

        public List<AuthorGetDTO> GetAll()
        {
            return _authorRepository.GetAllAsQuery()
                  .Include(x => x.Books) 
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
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _authorRepository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Author not found");
            AuthorGetDTO authorGetDTO = new AuthorGetDTO 
            { 
                Name = author.Name,
                Id = author.Id
            };
            return authorGetDTO;
            

        }

        

        public void Update(int? id, AuthorUpdateDTO authorUpdateDTO)
        {
            if (id is null || id < 1) throw new InvalidIdException("Id is invalid");
            if (authorUpdateDTO is null) throw new ArgumentNullException(nameof(authorUpdateDTO));
            if (string.IsNullOrWhiteSpace(authorUpdateDTO.Name)) throw new Exception("Cannot be null or empty");
           

            

            
            var author = _authorRepository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Author not found");
            author.Name = authorUpdateDTO.Name;
            _authorRepository.Commit();
        }

       
    }
}
