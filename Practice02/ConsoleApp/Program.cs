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

        private static GameSettings _settings;
        static void Main(string[] args)
        {
            _settings = GameConfigHandler.LoadConfig();

            Console.WriteLine($"Hello to {_settings.GameName}!");

            var gameMenu = new Menu(1)
            {
                Title = $"Start a new game of {_settings.GameName}",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1",
                        new MenuItem()
                        {
                            Title = "Computer starts",
                            CommandToExecute = TestGame
                        }
                    },
                    {
                        "2",
                        new MenuItem()
                        {
                            Title = "Human starts",
                            CommandToExecute = null
                        }
                    },
                    {
                        "3",
                        new MenuItem()
                        {
                            Title = "Human against Human",
                            CommandToExecute = null
                        }
                    }
                }
            };

            var Menu0 = new Menu()
            {
                Title = "Tic Tac Toe Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"S", new MenuItem()
                        {
                            Title = "Start game",
                            CommandToExecute = gameMenu.Run
                        }
                    },
                    {"J", new MenuItem()
                        {
                            Title = "Set defaults for game (save to JSON)",
                            CommandToExecute = SaveSettings
                        }
                    }
                }
            };

            Menu0.Run();
        }

        static string SaveSettings()
        {
            var done = false;

                Console.Clear();
                var boardWidth = 0;
                var boardHeight = 0;
                var userCanceled = false;

                (boardWidth, userCanceled) = GetUserIntInput("Enter board width", 3, 20, 0);
                if (userCanceled) return "";
                
                (boardHeight, userCanceled) = GetUserIntInput("Enter board height", 3, 20, 0);
                if (userCanceled) return "";
                    
                _settings.BoardHeight = boardHeight;
                _settings.BoardWidth = boardWidth;

                if (!userCanceled)
                {
                    GameConfigHandler.SaveConfig(_settings);
                }

                return "";
        }
        static string TestGame()
        {
            var game = new Game(_settings);
            var done = false;
            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                var userXint = 0;
                var userYint = 0;
                var userCanceled = false;

                (userXint, userCanceled) = GetUserIntInput("Enter X coordinate", 1, 7, 0);

                if (!userCanceled)
                {
                    (userYint, userCanceled) = GetUserIntInput("Enter Y coordinate", 1, 7, 0);
                }

                if (userCanceled)
                {
                    done = true;
                }
                else
                {
                    game.Move(userYint - 1, userXint - 1);
                }


            } while (!done);
            return "GAME OVER!";
        }

        
        static (int result, bool wasCanceled) GetUserIntInput(string prompt, int min, int max,
            int? cancelIntValue = null, string cancelStrValue = "")
        {
            do
            {
                Console.WriteLine(prompt);
                if (cancelIntValue.HasValue || !string.IsNullOrWhiteSpace(cancelStrValue))
                {
                    Console.WriteLine($"To cancel input enter: {cancelIntValue}" +
                                      $"{(cancelIntValue.HasValue && !string.IsNullOrWhiteSpace(cancelStrValue) ? " or " : "")}" +
                                      $"{cancelStrValue}");
                }

                Console.Write(">");
                var consoleLine = Console.ReadLine();

                if (consoleLine == cancelStrValue) return (0, true);

                if (int.TryParse(consoleLine, out var userInt))
                {
                    return userInt == cancelIntValue ? (userInt, true) : (userInt, false);
                }

                Console.WriteLine($"'{consoleLine}' cant be converted to int value!");
            } while (true);
        }

    }
}