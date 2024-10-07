namespace BookStore.Core.Models
{
    public class Book
    {
        public const int MAX_TITLE_LENGHT = 100;

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public decimal Price { get; }

        private Book(Guid id, string title, string description, decimal price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        public static (Book book, string Error) Create(Guid id, string title, string description, decimal price)
        {
            string error = string.Empty;

            if (title.Length > MAX_TITLE_LENGHT || string.IsNullOrEmpty(title))
            {
                error = "Book title can not be empty or longer, than 100 sybmols";
            }

            Book newBook = new Book(id, title, description, price);

            return (newBook, error);
        }
    }
}
