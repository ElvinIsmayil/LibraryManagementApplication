using Project___ConsoleApp__Library_Management_Application_.DTOs.Author;

namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Book
{
    public class BookGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }
        public List<AuthorGetBookDTO> Authors { get; set; }

        public override string ToString()
        {
            return $"Book Id: {Id} , Title: {Title} , Description: {Description} , PublishedYear: {PublishedYear} ";
        }
    }

    public class AuthorGetBookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return $"Author Id: {Id} , Name: {Name}";
        }
    }
}
