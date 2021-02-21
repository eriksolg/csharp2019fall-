using System;
using System.Collections.Generic;

namespace MenuSystem
{
    public class Menu
    {
        public string Title { get; set; }

        public List<MenuItem> MenuItems { get; set; }
            = new List<MenuItem>();

        public void Run()
        {
            var command = "";
            do
            {
                Console.WriteLine(Title);
                Console.WriteLine("====================");
                foreach (var menuItem in MenuItems)
                {
                    Console.WriteLine(menuItem.Command + " " + menuItem.Title);
                }
                Console.WriteLine("R return to previous");
                Console.WriteLine("X Exit");
                Console.WriteLine("----------");
                Console.Write(">");
            } while (command != "X");
        }
    }
}