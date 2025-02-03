using Project___ConsoleApp__Library_Management_Application_.Entities;

namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Loan
{
    public class LoanUpdateDTO
    {
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}
