using BooksStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.DAL
{
    public class BookStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public BookStoreDbContext(DbContextOptions options) : base(options) { }

        // Configures the database schema, relationships, constraints, and data seeding using the Fluent API.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.BookName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}