namespace Project___ConsoleApp__Library_Management_Application_.Entities
{
    public class Loan : BaseEntity
    {
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public List<LoanItem> LoanItems { get; set; }

    }
}
