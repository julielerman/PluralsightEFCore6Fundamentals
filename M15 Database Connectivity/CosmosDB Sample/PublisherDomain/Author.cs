namespace PublisherDomain
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
            AuthorId=Guid.NewGuid();
        }
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Book> Books { get; set; }
    }
}
