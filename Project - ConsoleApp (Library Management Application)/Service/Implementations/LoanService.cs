using Project___ConsoleApp__Library_Management_Application_.DTOs.Loan;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        public LoanService(ILoanRepository repository)
        {
            _loanRepository = repository;
        }
        public void Create(LoanCreateDTO loanCreateDTO)
        {
            if (loanCreateDTO is null) throw new EntityNotFoundException($"Loan not found");
            Loan loan = new Loan();
            loan.BorrowerId = loanCreateDTO.BorrowerId;
            loan.ReturnDate = loanCreateDTO.ReturnDate;
            loan.MustReturnDate = loanCreateDTO.MustReturnDate;

            _loanRepository.Add(loan);
            _loanRepository.Commit();
            
        }

        public void Delete(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var loan = _loanRepository.GetById((int)id);
            if (loan is null) throw new EntityNotFoundException("Loan not found");
            _loanRepository.Remove(loan);
            _loanRepository.Commit();
        }

        public List<LoanGetDTO> GetAll()
        {
            List<LoanGetDTO> mappedLoans = new List<LoanGetDTO>();
            List<Loan> loans = _loanRepository.GetAll();
            foreach (var borrower in loans)
            {
                LoanGetDTO loanGetDTO = new LoanGetDTO();
                loanGetDTO.BorrowerId = borrower.BorrowerId;
                loanGetDTO.ReturnDate = borrower.ReturnDate;
                loanGetDTO.MustReturnDate= borrower.MustReturnDate;
                

                mappedLoans.Add(loanGetDTO);
            }

            return mappedLoans;
        }

        public LoanGetDTO GetById(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var loan = _loanRepository.GetById((int)id);
            if (loan is null) throw new EntityNotFoundException("Loan not found");
            LoanGetDTO loanGetDTO = new LoanGetDTO()
            {
                MustReturnDate = loan.MustReturnDate,
                ReturnDate = loan.ReturnDate,
                LoanDate = loan.LoanDate,
                
                BorrowerId = loan.BorrowerId
            };
            return loanGetDTO;
        }
        
        public void Update(int? id, LoanUpdateDTO LoanUpdateDTO)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var loan = _loanRepository.GetById((int)id);
            if (loan is null) throw new EntityNotFoundException("Loan not found");
            loan.LoanDate = LoanUpdateDTO.LoanDate;
            loan.MustReturnDate = LoanUpdateDTO.MustReturnDate;
            loan.ReturnDate = LoanUpdateDTO.ReturnDate;

            _loanRepository.Commit();
        }
    }
}
