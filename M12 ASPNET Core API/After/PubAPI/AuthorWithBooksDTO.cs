namespace PubAPI
{
    public class AuthorWithBooksDTO
    {
        public AuthorWithBooksDTO()
        {
            Books = new List<BooksDTO>();
        }
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BooksDTO> Books { get; set; }
    }

    public class BooksDTO
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal BasePrice { get; set; }
        public int AuthorId { get; set; }
    }
}
