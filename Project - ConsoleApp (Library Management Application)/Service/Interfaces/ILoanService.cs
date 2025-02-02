using Project___ConsoleApp__Library_Management_Application_.DTOs.Loan;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public interface ILoanService
    {
        void Create(LoanCreateDTO loanCreateDTO);
        void Delete(int? id);
        void Update(int? id, LoanUpdateDTO LoanUpdateDTO);
        LoanGetDTO GetById(int? id);
        List<LoanGetDTO> GetAll();

    }
}
