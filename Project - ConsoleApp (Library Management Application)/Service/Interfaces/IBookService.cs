using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public interface IBookService
    {
        void Create(BookCreateDTO bookCreateDTO);
        void Delete(int id);
        void Update(int id, BookUpdateDTO bookUpdateDTO);
        BookGetDTO GetById(int id);
        List<BookGetDTO> GetAll();

    }
}
