namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Borrowers
{
    public class BorrowerGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<LoanBorrowerGetDTO> Loans { get; set; }

        public override string ToString()
        {
            return $"Borrower Id: {Id} , Name: {Name} , Email: {Email}";
        }
    }

    public class LoanBorrowerGetDTO
    {
        public int BorrowerId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime MustReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public override string ToString()
        {
            return $"Loan Id: {BorrowerId} , LoanDate: {LoanDate} , MustReturnDate: {MustReturnDate} , ReturnDate: {ReturnDate}";
        }
    }
}
