using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;

namespace Project___ConsoleApp__Library_Management_Application_.DTOs.LoanItem
{
    public class LoanItemGetDTO
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public BookGetDTO Book { get; set; } 
        public int BookId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
