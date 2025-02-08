using Project___ConsoleApp__Library_Management_Application_.DTOs.Borrowers;
using Project___ConsoleApp__Library_Management_Application_.DTOs.LoanItem;
using Project___ConsoleApp__Library_Management_Application_.Entities;
namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Loan
{
    public class LoanGetDTO
    {
        public int Id { get; set; }
        public int BorrowerId { get; set; }
        public BorrowerGetDTO Borrower { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public List<LoanItemGetDTO> LoanItems { get; set; }

    }
}
