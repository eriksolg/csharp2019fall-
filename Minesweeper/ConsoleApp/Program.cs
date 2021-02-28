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

        static string GameLoop()
        {
            var game = new Game();
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
                
                done = userYInt == 0 &&
                       userXInt == 0;

            } while (!done);
            return "GAME OVER!";
        }
    }
}