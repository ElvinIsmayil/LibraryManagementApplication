using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public void Create(AuthorCreateDTO authorCreateDTO)
        {
            if (authorCreateDTO is null) throw new EntityNotFoundException($"Author not found");
            if (string.IsNullOrWhiteSpace(authorCreateDTO.Name)) throw new ArgumentNullException("Author name is null or empty");
            Author author = new Author();
            author.Name = authorCreateDTO.Name;

            _repository.Add(author);
            _repository.Commit();


        }

        public void Delete(int? id)
        {
            if (id is null|| id<1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _repository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Author not found");
            _repository.Remove(author);
            _repository.Commit();
        }

        public List<AuthorGetDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public AuthorGetDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, AuthorUpdateDTO authorUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
