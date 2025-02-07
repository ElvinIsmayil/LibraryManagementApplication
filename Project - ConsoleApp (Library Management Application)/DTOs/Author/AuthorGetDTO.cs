namespace Project___ConsoleApp__Library_Management_Application_.DTOs.Author
{
    public class AuthorGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookGetAuthorDTO> Books { get; set; }

        public override string ToString()
        {
            return $"Author Id: {Id} , Name: {Name}";
        }
    }

    public class BookGetAuthorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PublishedYear { get; set; }


        public override string ToString()
        {
            return $"Book Id: {Id} , Title: {Title} , Description: {Description} , PublishedYear: {PublishedYear}";
        }
    }
}


