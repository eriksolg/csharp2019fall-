using System;
using System.Collections.Generic;

namespace MenuSystem
{
    public class Menu
    {
        public string Title { get; set; }
        private int _menuLevel;

        public Menu(int menuLevel = 0)
        {
            _menuLevel = menuLevel;
        }

        public List<MenuItem> MenuItems { get; set; }
            = new List<MenuItem>();

        public MenuItem MenuItemExit { get; set; } = new MenuItem() {Command = "X", Title = "Exit"};
        public MenuItem MenuItemReturnToPrevious { get; set; } = new MenuItem() {Command = "R", Title = "Return to Previous menu"};
        public MenuItem MenuItemReturnToMain { get; set; } = new MenuItem() {Command = "M", Title = "Return to Main Menu"};

        public string Run()
        {
            var command = "";
            do
            {
                Console.WriteLine(Title);
                Console.WriteLine("====================");
                foreach (var menuItem in MenuItems)
                {
                    Console.WriteLine(menuItem);
                }
                
                if (_menuLevel >= 2)
                {
                    Console.WriteLine(MenuItemReturnToPrevious);

                }
                
                if (_menuLevel >= 1)
                {
                    Console.WriteLine(MenuItemReturnToMain);
                }
                
                Console.WriteLine(MenuItemExit);
                Console.WriteLine("----------");
                Console.Write(">");

                command = Console.ReadLine()?.Trim().ToUpper() ?? "";

                var returnCommand = "";
                foreach (var menuItem in MenuItems)
                {
                    if (menuItem.Command == command &&
                        menuItem.CommandToExecute != null)
                    {
                        returnCommand = menuItem.CommandToExecute();
                        break;
                    }
                }

                if (returnCommand == MenuItemExit.Command)
                {
                    command = MenuItemExit.Command;
                }

                if (returnCommand == MenuItemReturnToMain.Command)
                {
                    if (_menuLevel != 0)
                    {
                        command = MenuItemReturnToMain.Command;
                    }
                }

            } while (command != MenuItemExit.Command &&
                     command != MenuItemReturnToMain.Command &&
                     command != MenuItemReturnToPrevious.Command);

            return command;
        }
    }
}