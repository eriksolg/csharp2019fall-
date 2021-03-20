using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<ContactType> ContactTypes { get; set; } = default!;

        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite(@"Data Source=/home/erik/Documents/Git/csharp2019fall/EFDemo1/ConsoleApp/contacts.db")
                .UseLoggerFactory(MyLoggerFactory);
        }
    }
}