using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using System.Reflection;

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
            optionsBuilder.UseSqlServer("Server=DESKTOP-CIU9UU4\\SQLEXPRESS;Database=LibraryManagmentApplication;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Book>()
             .HasMany(b => b.Authors)
             .WithMany(a => a.Books)
             .UsingEntity<Dictionary<string, object>>(
                 "BookAuthors",
                 j => j.HasOne<Author>().WithMany().HasForeignKey("AuthorId").OnDelete(DeleteBehavior.Cascade),
                 j => j.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.Cascade)
             );
        }
    }

    }

