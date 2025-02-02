using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public interface IAuthorService
    {
        void Create(AuthorCreateDTO authorCreateDTO);
        void Delete(int? id);
        void Update(int? id, AuthorUpdateDTO authorUpdateDTO);
        AuthorGetDTO GetById(int? id);
        List<AuthorGetDTO> GetAll();

    }
}
