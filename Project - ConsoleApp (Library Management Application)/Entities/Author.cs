namespace Project___ConsoleApp__Library_Management_Application_.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
        
    }
}
