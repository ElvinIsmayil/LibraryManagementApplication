using Project___ConsoleApp__Library_Management_Application_.Entities;

namespace Project___ConsoleApp__Library_Management_Application_.Repository.Interfaces
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : BaseEntity, new()
        {
            void Add(T entity);
            void Remove(T entity);
            T GetById(int id);
            List<T> GetAll();
            int Commit();

        }
    }
}
