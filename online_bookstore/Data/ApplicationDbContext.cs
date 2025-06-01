using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using online_bookstore.Model;
using System.Reflection.Metadata;


namespace online_bookstore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>()
               .HasMany(e => e.Books)
               .WithOne(e => e.Genre)
               .HasForeignKey(e => e.GenreId)
               .IsRequired();

            modelBuilder.Entity<Author>()
              .HasMany(e => e.Books)
              .WithOne(e => e.Author)
              .HasForeignKey(e => e.AuthorId)
              .IsRequired();

        }

        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = default!;
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;


    }
}
