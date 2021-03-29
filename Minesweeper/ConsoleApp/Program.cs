using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleUI;
using Domain;
using GameEngine;
using MenuSystem;

namespace ConsoleApp
{
    static class Program
    {
        private static SavedGame? _savedGame { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Minesweeper");

            var menuStartGame = new Menu(1)
            {
                Title = "Select Difficulty",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"1", new MenuItem()
                        {
                            Title = "Easy",
                            CommandToExecute = () => Play(Difficulty.Easy)
                        }
                    },
                    {"2", new MenuItem()
                        {
                            Title = "Medium",
                            CommandToExecute = () => Play(Difficulty.Medium) 
                        }
                    },
                    {"3", new MenuItem()
                        {
                            Title = "Hard",
                            CommandToExecute = () => Play(Difficulty.Hard) 
                        }
                    }
                }
            };

            var menu0 = new Menu(0)
            {
                Title = "Minesweeper Main Menu",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"S", new MenuItem()
                        {
                            Title = "Start a new game",
                            CommandToExecute = menuStartGame.Run 
                        }
                    },
                    {"L", new MenuItem()
                        {
                            Title = "Load saved game",
                            CommandToExecute = () =>
                            {
                                LoadSavedGame();
                                return "M";
                            }
                        }
                    },
                }
            };
            
            menu0.Run();
        }
        
        private static string Play(Difficulty difficulty = Difficulty.Easy)
        {
            var game = new Game(difficulty, _savedGame);
            UserAction userAction;
            
            var actionMenu = new Menu(0)
            {
                Title = "Action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"1", new MenuItem()
                        {
                            Title = "Open",
                            CommandToExecute = () =>
                            {
                                userAction = UserAction.Open;
                                return "X";
                            }
                        }
                    },
                    {"2", new MenuItem()
                        {
                            Title = "Mark/Unmark",
                            CommandToExecute = () =>
                            { 
                                userAction = UserAction.Mark;
                                return "X";
                            }
                        }
                    },
                    {"S", new MenuItem()
                        {
                            Title = "Save game",
                            CommandToExecute = () =>
                            {
                                SaveGame(game);
                                return "";
                            }
                        }
                    }
                }
            };

            do
            {
                Console.Clear();
                GameUI.PrintBoard(game);
                
                var userYInt = GetUserIntInput("Please enter Y value!", 0, game.BoardHeight - 1);
                var userXInt = GetUserIntInput("Please enter X value!", 0, game.BoardWidth - 1);

                userAction = UserAction.None;
                actionMenu.Run();

                switch (userAction)
                {
                    case UserAction.Open:
                        game.OpenCell(userYInt, userXInt);
                        break;
                    case UserAction.Mark:
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

            _savedGame = null;

            return "X";
        }
        private static int GetUserIntInput(string prompt, int min, int max)
        {
            do
            {
                Console.WriteLine(prompt);

                Console.Write(">");
                var consoleLine = Console.ReadLine();
                
                if (int.TryParse(consoleLine, out var userInt))
                {
                    if (userInt >= min && userInt <= max)
                        return userInt;
                    
                    Console.WriteLine($"{userInt} is not within the bounds!");
                    continue;
                }

                Console.WriteLine($"'{consoleLine}' cant be converted to int value!");
            } while (true);
        }

        private static void SaveGame(Game game)
        {
            if (game.GameStatus == GameStatus.NotStarted)
            {
                Console.WriteLine("Please make the first move before saving!");
                return;
            }
            var saveActionMenu = new Menu()
            {
                Title = "Save Game",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                { 
                    {"1", new MenuItem()
                        {
                            Title = "Save a new game",
                            CommandToExecute = () => SaveGame(false)
                        }
                    },
                    {"2", new MenuItem()
                        {
                            Title = "Overwrite an existing saved game",
                            CommandToExecute = () => SaveGame(true)
                        }
                    }
                }
            };

            String SaveGame(Boolean overrideExistingSave)
            {
                var savedGame = new SavedGame()
                {
                    DateTime = new DateTime(),
                    Board = game.GetBoard(),
                    BoardHeight = game.BoardHeight,
                    BoardWidth = game.BoardWidth,
                    GameStatus = game.GameStatus
                };

                var saveGameSelectMenu = new Menu();

                if (overrideExistingSave)
                {
                    var savedGames = SavedGameHandler.GetSavedGames();

                    if (!savedGames.Any())
                    {
                        Console.WriteLine("No saved games found!");
                        return "";
                    }

                    var i = 1;
                    foreach (var existingSavedGame in savedGames)
                    {
                        saveGameSelectMenu.addMenuItem(i.ToString(), new MenuItem(){
                            Title = existingSavedGame.DateTime.ToString(),
                            CommandToExecute = () =>
                            {
                                SavedGameHandler.SaveGame(savedGame, existingSavedGame);
                                return "X";
                            }
                        });
                        i++;
                    }

                    return saveGameSelectMenu.Run();
                }

                SavedGameHandler.SaveGame(savedGame);
                return "X";

            }

            saveActionMenu.Run();
        }
        private static void LoadSavedGame()
        {
            var loadGameMenu = new Menu(1);

            var savedGames = SavedGameHandler.GetSavedGames();

            if (!savedGames.Any())
            {
                Console.WriteLine("No saved games found!");
                return;
            }

            var i = 1;
            foreach (var existingSavedGame in savedGames)
            {
                loadGameMenu.addMenuItem(i.ToString(), new MenuItem(){
                    Title = existingSavedGame.DateTime.ToString(),
                    CommandToExecute = () =>
                    {
                        _savedGame = existingSavedGame;
                        return "X";
                    }
                });
                i++;
            }

            loadGameMenu.Run();
            Play();
        }
    }
}