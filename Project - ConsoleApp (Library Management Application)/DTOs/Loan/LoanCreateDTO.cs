using Project___ConsoleApp__Library_Management_Application_.Entities;

namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Author
{
    public class LoanCreateDTO
    {
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
