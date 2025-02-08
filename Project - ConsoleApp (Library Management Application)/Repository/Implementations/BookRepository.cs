using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces;

namespace Project___ConsoleApp__Library_Management_Application_.Repository.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookRepository()
        => _appDbContext = new AppDbContext();
        public List<Author>? AuthorsSet(List<int> authorId)
       => _appDbContext.Authors.Where(a => authorId
                               .Contains(a.Id))
                               .ToList();
        public IQueryable<Book> GetAllAsQuery()
        {
            return _appDbContext.Books.Include(b => b.Authors);
        }



    }
}
