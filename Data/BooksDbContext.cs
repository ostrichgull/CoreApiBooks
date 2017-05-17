using Microsoft.EntityFrameworkCore;
using CoreApiBooks.Models;

namespace CoreApiBooks.Data
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable(nameof(Genres));
            modelBuilder.Entity<PersonType>().ToTable(nameof(PersonTypes));
            modelBuilder.Entity<Gender>().ToTable(nameof(Genders));
            modelBuilder.Entity<Country>().ToTable(nameof(Countries));
            modelBuilder.Entity<State>().ToTable(nameof(States));
            modelBuilder.Entity<City>().ToTable(nameof(Cities));
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
