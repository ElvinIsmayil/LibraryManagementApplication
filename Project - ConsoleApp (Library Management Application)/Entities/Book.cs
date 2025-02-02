namespace Project___ConsoleApp__Library_Management_Application_.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }
        public List<Author> Authors { get; set; }
    }
}
