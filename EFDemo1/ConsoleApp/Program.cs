using System;
using System.Linq;
using DAL;
using Domain;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello EF Core!");

            using (var ctx = new AppDbContext())
            {

                ctx.Persons.Add(new Person()
                {
                    FirstName = "Person1",
                    LastName = "Person1last"
                });
                
                ctx.Persons.Add(new Person()
                {
                    FirstName = "Person2",
                    LastName = "Person2last"
                });

                ctx.SaveChanges();
                
                foreach (var person in ctx.Persons.Where(p => p.FirstName == "Person1"))
                {
                    Console.WriteLine(person);
                }
            }
        }
    }
}