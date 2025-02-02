namespace Project___ConsoleApp__Library_Management_Application_.Entities
{
    public class LoanItem : BaseEntity
    {
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
