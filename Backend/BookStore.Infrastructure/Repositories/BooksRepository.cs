using BookStore.Infrastructure.Entities;
using BookStore.Core.Models;
using BookStore.Core.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookStoreDbContext _dbContext;

        public BooksRepository(BookStoreDbContext dbContext) => _dbContext = dbContext;

        public async Task<Guid> Create(Book book)
        {
            var newBook = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };

            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.Books.Where(b => b.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<Book?> GetById(Guid id)
        {
            var bookEntity = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);

            if (bookEntity == null)
            {
                return null;
            }

            Book book = Book.Create(bookEntity.Id, bookEntity.Title,
                bookEntity.Description, bookEntity.Price).book;

            return book;
        }

        public async Task<List<Book>> GetAll()
        {
            var bookEntities = await _dbContext.Books.AsNoTracking().ToListAsync();
            var books = bookEntities.Select(b => Book.Create(b.Id,
                b.Title, b.Description, b.Price).book).ToList();

            return books;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _dbContext.Books.Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Price, b => price));

            return id;
        }
    }
}
