using System;
using System.Collections.Generic;

namespace MenuSystem
{
    public class Menu
    {
        public string Title { get; set; }
        private int _menuLevel;
        private const string MenuCommandExit = "X";
        private const string MenuCommandReturnToPrevious = "P";
        private const string MenuCommandReturnToMain = "M";
        private Dictionary<string, MenuItem> _menuItemsDictionary = new Dictionary<string, MenuItem>();

        public Menu(int menuLevel = 0)
        {
            _menuLevel = menuLevel;
        }

        public Dictionary<string, MenuItem> MenuItemsDictionary
        {
            get => _menuItemsDictionary;
            set
            {
                _menuItemsDictionary = value;
                if (_menuLevel >= 2)
                {
                    _menuItemsDictionary.Add(MenuCommandReturnToPrevious, MenuItemReturnToPrevious);
                }
                
                if (_menuLevel >= 1)
                {
                    _menuItemsDictionary.Add(MenuCommandReturnToMain, MenuItemReturnToMain);
                }
                
                _menuItemsDictionary.Add(MenuCommandExit, MenuItemExit);
            }
        }

        public void addMenuItem(string menuCommand, MenuItem menuItem)
        {
            _menuItemsDictionary.Add(menuCommand, menuItem);
        }

        public MenuItem MenuItemExit { get; set; } = new MenuItem() {Title = "Exit"};
        public MenuItem MenuItemReturnToPrevious { get; set; } = new MenuItem() {Title = "Return to Previous menu"};
        public MenuItem MenuItemReturnToMain { get; set; } = new MenuItem() {Title = "Return to Main Menu"};

        public string Run()
        {
            var command = "";
            do
            {
                Console.WriteLine(Title);
                Console.WriteLine("====================");
                foreach (var menuItem in MenuItemsDictionary)
                {
                    Console.Write(menuItem.Key);
                    Console.Write(" ");
                    Console.WriteLine(menuItem.Value);
                }
                Console.WriteLine("----------");
                Console.Write(">");

                command = Console.ReadLine()?.Trim().ToUpper() ?? "";

                var returnCommand = "";

                if (MenuItemsDictionary.ContainsKey(command))
                {
                    var menuItem = MenuItemsDictionary[command];
                    if (menuItem.CommandToExecute != null)
                    {
                        returnCommand = menuItem.CommandToExecute();
                    }
                }

                if (returnCommand == MenuCommandExit)
                {
                    command = MenuCommandExit;
                }

                if (returnCommand == MenuCommandReturnToMain)
                {
                    if (_menuLevel != 0)
                    {
                        command = MenuCommandReturnToMain;
                    }
                }

            } while (command != MenuCommandExit &&
                     command != MenuCommandReturnToMain &&
                     command != MenuCommandReturnToPrevious);

            return command;
        }
    }
}