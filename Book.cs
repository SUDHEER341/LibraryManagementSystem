namespace LibraryManagementSystem
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsBorrowed { get; set; }

        public Book(int id, string title, string author, string genre, bool isBorrowed)
        {
            BookId = id;
            Title = title;
            Author = author;
            Genre = genre;
            IsBorrowed = isBorrowed;
        }
    }
}
