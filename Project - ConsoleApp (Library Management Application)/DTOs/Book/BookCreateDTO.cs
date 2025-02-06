namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Book
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }

        public List<int> AuthorIds { get; set; }


    }
}
