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
            List<BorrowerGetDTO> mappedBorrowers = new List<BorrowerGetDTO>();
            List<Borrower> borrowers = _borrowerRepository.GetAll();
            foreach (var borrower in borrowers)
            {
                BorrowerGetDTO borrowerGetDTO = new BorrowerGetDTO();
                borrowerGetDTO.Id = borrower.Id;
                borrowerGetDTO.Name = borrower.Name;
                borrowerGetDTO.Email = borrower.Email;

                mappedBorrowers.Add(borrowerGetDTO);
            }

            return mappedBorrowers;
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
