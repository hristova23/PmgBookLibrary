using Library.Data.Configuration;
using Library.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new BookConfiguration());

            builder.Entity<ApplicationUser>(user =>
            {
                user
                    .HasMany(s => s.SendedTransactions)
                    .WithOne(s => s.Sender)
                    .HasForeignKey(s => s.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                user
                    .HasMany(s => s.RecievedTransactions)
                    .WithOne(s => s.Reciever)
                    .HasForeignKey(s => s.RecieverId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(builder);
        }

        public DbSet<Book> Books { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Transaction> Transaction { get; set; } = null!;
    }
}