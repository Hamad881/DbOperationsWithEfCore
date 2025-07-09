using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEfCoreApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {  
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Currenc = "PKR", Description = "Pakistani Ruppee" },
                new Currency() { Id = 2, Currenc = "Dollar", Description = "US Dollar" },
                new Currency() { Id = 3, Currenc = "Euro", Description = "Euro" },
                new Currency() { Id = 4, Currenc = "Dinar", Description = "Quwaiti Dinar" },
                new Currency() { Id = 5, Currenc = "Dinar", Description = "Dinar" }

                );
            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "Urdu", Description = "Urdu" },
                new Language() { Id = 2, Title = "Hindi", Description = "Hindi" },
                new Language() { Id = 3, Title = "English", Description = "American English" },
                new Language() { Id = 4, Title = "French", Description = "French" }
                );

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        
        public DbSet<BookPrice> BooksPrice { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Author> Authors { get; set; }
        
    }
}
