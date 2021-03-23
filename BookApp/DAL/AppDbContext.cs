using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}