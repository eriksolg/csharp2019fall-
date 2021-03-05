using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<ContactType> ContactTypes { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=/home/erik/Documents/Git/csharp2019fall/EFDemo1/ConsoleApp/contacts.db");
        }
    }
}