using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ConsoleUI;
using GameEngine;
using MenuSystem;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Minesweeper");

            var Menu0 = new Menu()
            {
                Title = "Minesweeper Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"S", new MenuItem()
                        {
                            Title = "Start game",
                            CommandToExecute = GameLoop 
                        }
                    }
                }
            };

            Menu0.Run();
        }

        private static string GameLoop()
        {
            
            var ActionMenu = new Menu()
            {
                Title = "Action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"1", new MenuItem()
                        {
                            Title = "Open",
                            CommandToExecute = () => "1"
                        }
                    },
                    {"2", new MenuItem()
                        {
                            Title = "Mark",
                            CommandToExecute = () => "2"
                        }
                    }
                }
            };
            
            var game = new Game();
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                var userXInt = -1;
                var userYInt = -1;
                var userActionInt = -1;
                do
                {
                    Console.WriteLine("Give me Y value!");
                    Console.Write(">");
                    var userY = Console.ReadLine();
                    if (!int.TryParse(userY, out userYInt))
                    {
                        Console.WriteLine($"{userY} is not a number");

                    }
                } while (userYInt < 0);
                
                do
                {
                    Console.WriteLine("Give me X value!");
                    Console.Write(">");
                    var userX = Console.ReadLine();
                    if (!int.TryParse(userX, out userXInt))
                    {
                        Console.WriteLine($"{userX} is not a number");
                    }
                } while (userXInt < 0);
                
                do
                {
                    var userAction = ActionMenu.Run();
                    if (!int.TryParse(userAction, out userActionInt))
                    {
                        Console.WriteLine($"{userAction} is not a number");
                    }
                } while (userActionInt < 0);

                switch (userActionInt)
                {
                    case 1:
                        game.OpenCell(userYInt, userXInt);
                        break;
                    case 2:
                        game.MarkCell(userYInt, userXInt);
                        break;
                }

            } while (true);
        }
    }
}