using BookStore.Infrastructure.Configurations;
using BookStore.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        public DbSet<BookEntity> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }
    }
}
