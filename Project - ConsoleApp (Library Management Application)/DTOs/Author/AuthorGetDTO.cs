using Project___ConsoleApp__Library_Management_Application_.DTOs.Book;

namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Author
{
    public class AuthorGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGetAuthorDTO> BookGetAuthorDTO { get; set; }
    }

    public class BookGetAuthorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }
    }
}
