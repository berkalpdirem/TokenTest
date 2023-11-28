using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TokenTest.Data.Entity;

namespace TokenTest.Context
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<FavBook> FavBooks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Book>().HasKey(x => x.ID);
            mb.Entity<FavBook>().HasKey(x => x.ID);

            mb.Entity<Book>()
              .HasMany(x => x.FavBooks)
              .WithOne(x => x.Book)
              .HasForeignKey(x => x.BookID);

            mb.Entity<AppUser>()
              .HasMany(x => x.FavBooks)
              .WithOne(x => x.AppUser)
              .HasForeignKey(x => x.AppUserID);

            base.OnModelCreating(mb);
        }

    }
}
