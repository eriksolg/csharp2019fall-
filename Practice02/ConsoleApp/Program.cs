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
            Console.WriteLine("Hello Game!");
            
            var Menu2 = new Menu(2)
            {
                Title = "Menu 2",
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Command = "C",
                        Title = "item C",
                        CommandToExecute = null
                    }
                }
            };
            
            var Menu1 = new Menu(1)
            {
                Title = "Menu 1",
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Command = "C",
                        Title = "Go to level 2",
                        CommandToExecute = Menu2.Run
                    }
                }
            };

            var gameMenu = new Menu()
            {
                Title = "Start a new game of Tic-Tac-Toe",
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Command = "1",
                        Title = "Computer starts",
                        CommandToExecute = TestGame
                    },
                    new MenuItem()
                    {
                        Command = "2",
                        Title = "Human starts",
                        CommandToExecute = null
                    },
                    new MenuItem()
                    {
                        Command = "3",
                        Title = "Human against Human",
                        CommandToExecute = null
                    }
                }
            };

            var Menu0 = new Menu()
            {
                Title = "Tic Tac Toe Main Menu",
                MenuItems = new List<MenuItem>()
                {
                    new MenuItem()
                    {
                        Command = "S",
                        Title = "Start game",
                        CommandToExecute = gameMenu.Run
                    }
                }
            };

            Menu0.Run();
        }

        static string TestGame()
        {
            var game = new Game(7, 7);
            var done = false;
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                var userXInt = -1;
                var userYInt = -1;
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
                
                game.Move(userYInt, userXInt);

                done = userYInt == 0 &&
                       userXInt == 0;

            } while (!done);
            return "GAME OVER!";
        }
    }
}