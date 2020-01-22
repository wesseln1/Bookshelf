using System;
using System.Collections.Generic;
using System.Text;
using BookShelf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<BookShelf.Models.Author> Author { get; set; }

        public DbSet<BookShelf.Models.Book> Book { get; set; }

        public DbSet<BookShelf.Models.Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create some Authors
            Author author1 = new Author
            {
                Id = 1,
                Name = "Jimmy John",
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Author>().HasData(author1);

            Author author2 = new Author
            {
                Id = 2,
                Name = "Jersey Mike",
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Author>().HasData(author2);

            Author author3 = new Author
            {
                Id = 3,
                Name = "Jared FFogle",
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Author>().HasData(author3);

            // Create some Books
            Book johnBook = new Book
            {
                Id = 1,
                Title = "Free Smells",
                Genre = "Fantasy",
                YearPublished = 1999,
                AuthorId = author1.Id,
                Rating = 5,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Book>().HasData(johnBook);

            Book jerseyBook = new Book
            {
                Id = 2,
                Title = "Jersey Subs",
                Genre = "Cooking",
                YearPublished = 2001,
                AuthorId = author2.Id,
                Rating = 10,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Book>().HasData(jerseyBook);

            Book ffffffogleBook = new Book
            {
                Id = 3,
                Title = "How to make a prison sandwich",
                Genre = "Instructional",
                YearPublished = 2015,
                AuthorId = author1.Id,
                Rating = 3,
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Book>().HasData(ffffffogleBook);

            // Create comments
            Comment johnComment = new Comment
            {
                Id = 1,
                BookId = johnBook.Id,
                Text = "I do love Jimmy Johns!",
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Comment>().HasData(johnComment);

            Comment jerseyComment = new Comment
            {
                Id = 2,
                BookId = jerseyBook.Id,
                Text = "Firehouse subs is better!",
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Comment>().HasData(jerseyComment);

            Comment fffffogleComment = new Comment
            {
                Id = 3,
                BookId = ffffffogleBook.Id,
                Text = "Hes really get that footlong now...",
                ApplicationUserId = user.Id
            };
            modelBuilder.Entity<Comment>().HasData(fffffogleComment);
        }
    }
}
