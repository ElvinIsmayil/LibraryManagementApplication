using Microsoft.EntityFrameworkCore;
using Project___ConsoleApp__Library_Management_Application_.Data;
using Project___ConsoleApp__Library_Management_Application_.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces.IGenericRepository;

namespace Project___ConsoleApp__Library_Management_Application_.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository()
        => _appDbContext = new AppDbContext();


        public void Add(T entity)
            => _appDbContext.Set<T>().Add(entity);


        public int Commit()
        => _appDbContext.SaveChanges();


        public List<T> GetAll()
        {
            var query = _appDbContext.Set<T>(); 
            

            return query.ToList();
                
                
        }
        public IQueryable<T> GetAllAsQuery()
        {
            return _appDbContext.Set<T>().AsQueryable();
        }


        public T GetById(int id)
        => _appDbContext.Set<T>().FirstOrDefault(x => x.Id == id);

        public void Remove(T entity)
            => _appDbContext.Set<T>().Remove(entity);

    }
}
