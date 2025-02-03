using Project___ConsoleApp__Library_Management_Application_.DTOs.LoanItem;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public interface ILoanItemService
    {
        void Create(LoanItemCreateDTO loanItemCreateDTO);
        void Delete(int? id);
        void Update(int? id, LoanItemUpdateDTO loanItemUpdateDTO);
        LoanItemGetDTO GetById(int? id);
        List<LoanItemGetDTO> GetAll();

    }
}
