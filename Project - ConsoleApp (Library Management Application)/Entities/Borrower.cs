namespace Project___ConsoleApp__Library_Management_Application_.Entities
{
    public class Borrower : BaseEntity
    {
        public string Name{ get; set; }
        public string Email{ get; set; }

        public List<Loan> Loans { get; set; }
        
    }
}
