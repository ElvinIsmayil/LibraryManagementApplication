using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;
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
            return _loanItemRepository.GetAllAsQuery()
                .Include(li => li.Book)
                .Select(li => new LoanItemGetDTO
                {
                    Id = li.Id,
                    LoanId = li.LoanId,
                    BookId = li.BookId,
                    Book = new BookGetDTO
                    {
                        Id = li.Book.Id,
                        Title = li.Book.Title,
                        Description = li.Book.Description,
                        PublishedYear = li.Book.PublishedYear
                    },
                    IsDeleted = li.IsDeleted,
                    CreatedAt = li.CreatedAt,
                    UpdatedAt = li.UpdatedAt
                })
                .ToList();
        }

        
            public LoanItemGetDTO GetById(int? id)
        {
            if (id == null || id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid LoanItem ID.");

            var loanItem = _loanItemRepository.GetAllAsQuery()
                .Include(li => li.Book)
                .FirstOrDefault(li => li.Id == id);

            if (loanItem == null)
                throw new EntityNotFoundException("LoanItem not found.");

            return new LoanItemGetDTO
            {
                Id = loanItem.Id,
                LoanId = loanItem.LoanId,
                BookId = loanItem.BookId,
                Book = new BookGetDTO
                {
                    Id = loanItem.Book.Id,
                    Title = loanItem.Book.Title,
                    Description = loanItem.Book.Description,
                    PublishedYear = loanItem.Book.PublishedYear
                },
                IsDeleted = loanItem.IsDeleted,
                CreatedAt = loanItem.CreatedAt,
                UpdatedAt = loanItem.UpdatedAt
            };
        }



        public void Update(int? id, LoanItemUpdateDTO loanItemUpdateDTO)
        {
            if (id == null || id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid LoanItem ID.");
            if (loanItemUpdateDTO == null)
                throw new ArgumentNullException(nameof(loanItemUpdateDTO), "LoanItem update data is null.");

            var loanItem = _loanItemRepository.GetById((int)id);
            if (loanItem == null)
                throw new EntityNotFoundException("LoanItem not found.");

            loanItem.IsDeleted = loanItemUpdateDTO.IsDeleted;
            loanItem.UpdatedAt = DateTime.UtcNow;

            _loanItemRepository.Commit();

            Console.WriteLine($"Success: LoanItem ID {id} has been updated.");
        }

    }
}
