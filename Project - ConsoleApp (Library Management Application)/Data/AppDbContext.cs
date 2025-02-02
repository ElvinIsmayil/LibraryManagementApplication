using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entities;

namespace Project___ConsoleApp__Library_Management_Application_.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=JUPITER02\\MAIN;Database=LibraryManagmentApplication;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }



    }
}
