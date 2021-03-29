using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<SavedGame> SavedGames { get; set; } = default!;
        
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public AppDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("Server=homeserver;Database=minesweeper;User=minesweeper;Password=Minesweeper$123", new MySqlServerVersion(new Version(8, 0, 21)));;
        }
    }
}