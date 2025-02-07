using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Borrowers;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Exceptions;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Service.Interfaces
{
    public class BorrowerService : IBorrowerService
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerService(IBorrowerRepository repository)
        {
            _borrowerRepository = repository;
        }

        public void Create(BorrowerCreateDTO borrowerCreateDTO)
        {
            if (borrowerCreateDTO is null) throw new EntityNotFoundException($"Book not found");
            if (string.IsNullOrWhiteSpace(borrowerCreateDTO.Name)) throw new ArgumentNullException("Borrower name is null or empty");
            Borrower borrower = new Borrower();
            borrower.Email = borrowerCreateDTO.Email;
            borrower.Name = borrowerCreateDTO.Name;

            _borrowerRepository.Add(borrower);
            _borrowerRepository.Commit();

            
        }

        public void Delete(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var borrower = _borrowerRepository.GetById((int)id);
            if (borrower is null) throw new EntityNotFoundException("Borrower not found");
            _borrowerRepository.Remove(borrower);
            _borrowerRepository.Commit();
        }

        public List<BorrowerGetDTO> GetAll()
        {
            return _borrowerRepository.GetAllAsQuery()
                 .Include(x => x.Loans)
                 .Select(borrower => new BorrowerGetDTO
                 {
                     Id = borrower.Id,
                     Name = borrower.Name,
                     Email = borrower.Email,
                     Loans = borrower.Loans.Select(loan => new LoanBorrowerGetDTO
                     {
                        BorrowerId = loan.Id,
                        LoanDate = loan.LoanDate,
                        ReturnDate = loan.ReturnDate,
                        MustReturnDate = loan.MustReturnDate


                     }).ToList()
                 })
                 .ToList();
        }

        public BorrowerGetDTO GetById(int? id)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var borrower = _borrowerRepository.GetById((int)id);
            if (borrower is null) throw new EntityNotFoundException("Borrower not found");
            BorrowerGetDTO borrowerGetDTO = new BorrowerGetDTO()
            {
                Email = borrower.Email,
                Name = borrower.Name,
                Id = borrower.Id

            };
            return borrowerGetDTO;
        }

        public void Update(int? id, BorrowerUpdateDTO borrowerUpdateDTO)
        {
            if (id is null || id < 1) throw new ArgumentOutOfRangeException("Id is invalid");
            var borrower = _borrowerRepository.GetById((int)id);
            if (borrower is null) throw new EntityNotFoundException("Borrower not found");
            borrower.Email = borrowerUpdateDTO.Email;
            borrower.Name = borrowerUpdateDTO.Name;


            _borrowerRepository.Commit();
        }
    }
}
