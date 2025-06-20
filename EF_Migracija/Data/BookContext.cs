using Microsoft.EntityFrameworkCore;
using EF_Migracija.Models;

namespace EF_Migracija.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Book {get; set;}
        public DbSet<Author> Author { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                    new Author
                    {
                        AuthorId=1,
                        Name="William Shakespeare",
                        Bio="Engleski književnik, kazališni redatelj i ljubitelj muškaraca..."
                    }
                );

            modelBuilder.Entity<Book>().HasData(
                    new Book { BookId=1, AuthorId=1, Title="Romeo i Julia"},
                    new Book { BookId=2, AuthorId=1, Title="Hamlet"},
                    new Book { BookId=3, AuthorId=1, Title="Otelo"}
                );
        }
    }
}
