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
            List<AuthorGetDTO> mappedAuthors = new List<AuthorGetDTO>();
            List<Author> authors = _repository.GetAll();
            foreach (var author in authors)
            {
                AuthorGetDTO authorGetDTO = new AuthorGetDTO();
                authorGetDTO.Name = author.Name;
                authorGetDTO.Id = author.Id;
                mappedAuthors.Add(authorGetDTO);            
            }

            return mappedAuthors;
        }

        public AuthorGetDTO GetById(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _repository.GetById((int)id);
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
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var author = _repository.GetById((int)id);
            if (author is null) throw new EntityNotFoundException("Author not found");
            author.Name = authorUpdateDTO.Name;
            _repository.Commit();
        }

       
    }
}
