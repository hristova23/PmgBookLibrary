using Library.Data.Configuration;
using Library.Infrastructure.Data.Models;
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

            builder.Entity<Book>(book =>
            {
                book
                    .HasOne(c => c.Category)
                    .WithMany(b => b.Books)
                    .HasForeignKey(c => c.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<FavoriteBook>(favoriteBook =>
            {
                favoriteBook
                    .HasKey(fb => new { fb.UserId, fb.BookId });

                favoriteBook
                    .HasOne(a => a.User)
                    .WithMany(f => f.FavoriteBooks)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                favoriteBook
                    .HasOne(b => b.Book)
                    .WithMany(u => u.LikedByUsers)
                    .HasForeignKey(b => b.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<FinishedBook>(finishedBook =>
            {
                finishedBook
                    .HasKey(fb => new { fb.UserId, fb.BookId });

                finishedBook
                    .HasOne(a => a.User)
                    .WithMany(f => f.FinishedBooks)
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                finishedBook
                    .HasOne(b => b.Book)
                    .WithMany(u => u.ReadByUsers)
                    .HasForeignKey(b => b.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

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