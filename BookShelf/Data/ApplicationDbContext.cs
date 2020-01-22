using System;
using System.Collections.Generic;
using System.Text;
using BookShelf.Models;
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
    }
}
