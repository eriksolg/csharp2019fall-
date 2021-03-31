using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello EF Core!");

            using (var ctx = new AppDbContext())
            {

                InsertDataToDb(ctx);
                
                foreach (var person in ctx.Persons
                    .Include(p => p.Contacts)
                    .ThenInclude(c => c.ContactType)
                    .Where(p => p.FirstName == "Person1"))
                {
                    Console.WriteLine(person);
                    foreach (var contact in person.Contacts!)
                    {
                        Console.WriteLine($"\t{contact}");
                        Console.WriteLine($"\t{contact.ContactType}");
                        foreach (var contact2 in contact.ContactType.Contacts!)
                        {
                            Console.WriteLine(contact2);
                        }
                    }
                }
            }

            static void InsertDataToDb(AppDbContext ctx)
            {
                var skype = new ContactType()
                    {
                        ContactTypeValue = "skype"
                    };
                
                var email = new ContactType()
                    {
                        ContactTypeValue = "email"
                    };

                ctx.ContactTypes.Add(skype);
                ctx.ContactTypes.Add(email);
                
                Console.WriteLine(skype);

                ctx.Persons.Add(new Person()
                {
                    FirstName = "Person1",
                    LastName = "Person1last",
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactValue = "test",
                            ContactType = email
                        }
                    }
                });
                
                ctx.Persons.Add(new Person()
                {
                    FirstName = "Person2",
                    LastName = "Person2last",
                    Contacts = new List<Contact>()
                    {
                        new Contact()
                        {
                            ContactValue = "test",
                            ContactType = skype
                        }
                    }
                });

                ctx.SaveChanges();
                
                Console.WriteLine(skype);

            }
        }
    }
}