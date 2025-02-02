using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public interface IBorrowerService
    {

        void Create(BorrowerCreateDTO borrowerCreateDTO);
        void Delete(int id);
        void Update(int id, BorrowerUpdateDTO borrowerUpdateDTO);
        BorrowerGetDTO GetById(int id);
        List<BorrowerGetDTO> GetAll();

    }
}
