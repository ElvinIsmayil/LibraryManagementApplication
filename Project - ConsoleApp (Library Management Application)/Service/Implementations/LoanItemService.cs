using Project___ConsoleApp__Library_Management_Application_.DTOs.LoanItem;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class LoanItemService : ILoanItemService
    {
        private readonly ILoanItemRepository _loanItemRepository;

        public LoanItemService(ILoanItemRepository repository)
        {
            _loanItemRepository = repository;
        }
        
        public void Create(LoanItemCreateDTO loanItemCreateDTO)
        {
            if (loanItemCreateDTO is null) throw new EntityNotFoundException($"LoanItem not found");
            LoanItem loanItem = new LoanItem();
            loanItem.LoanId = loanItemCreateDTO.LoanId;
            loanItem.BookId = loanItemCreateDTO.BookId;
            
            _loanItemRepository.Add(loanItem);
            _loanItemRepository.Commit();

        }

        public void Delete(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var loanItem = _loanItemRepository.GetById((int)id);
            if (loanItem is null) throw new EntityNotFoundException("LoanItem not found");
            _loanItemRepository.Remove(loanItem);
            _loanItemRepository.Commit();
        }

        public List<LoanItemGetDTO> GetAll()
        {
            throw new NotImplementedException();

        }

        public LoanItemGetDTO GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(int? id, LoanItemUpdateDTO loanItemUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
