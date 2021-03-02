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

            var MenuStartGame = new Menu()
            {
                Title = "Select Difficulty",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"1", new MenuItem()
                        {
                            Title = "Easy",
                            CommandToExecute = () => GameLoop(Difficulty.Easy)
                        }
                    },
                    {"2", new MenuItem()
                        {
                            Title = "Medium",
                            CommandToExecute = () => GameLoop(Difficulty.Medium) 
                        }
                    },
                    {"3", new MenuItem()
                        {
                            Title = "Hard",
                            CommandToExecute = () => GameLoop(Difficulty.Hard) 
                        }
                    }
                }
            };
            
            var Menu0 = new Menu()
            {
                Title = "Minesweeper Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"S", new MenuItem()
                        {
                            Title = "Start game",
                            CommandToExecute = MenuStartGame.Run 
                        }
                    }
                }
            };

            Menu0.Run();
        }

        private static string GameLoop(Difficulty difficulty)
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
                            Title = "Mark/Unmark",
                            CommandToExecute = () => "2"
                        }
                    }
                }
            };
            
            var game = new Game(difficulty);
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
                        userYInt = -1;
                    }
                    if (userYInt >= game.BoardHeight)
                    {
                        Console.WriteLine("Y coordinate out of board bounds!");
                    }
                } while (userYInt < 0 || userYInt >= game.BoardHeight);
                
                do
                {
                    Console.WriteLine("Give me X value!");
                    Console.Write(">");
                    var userX = Console.ReadLine();
                    if (!int.TryParse(userX, out userXInt))
                    {
                        Console.WriteLine($"{userX} is not a number");
                        userXInt = -1;
                    }
                    if (userXInt >= game.BoardWidth)
                    {
                        Console.WriteLine("X coordinate out of board bounds!");
                    }
                } while (userXInt < 0 || userXInt >= game.BoardWidth);
                
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

                game.UpdateGameStatus();

            } while (game.GameStatus == GameStatus.InProgress);

            game.OpenAllCells();
            Console.Clear();
            GameUI.PrintBoard(game);

            switch (game.GameStatus)
            {
                case GameStatus.Lost:
                    Console.WriteLine("Game Lost!");
                    break;
                case GameStatus.Won:
                    Console.WriteLine("Game Won!");
                    break;
            }

            return "X";
        }
    }
}