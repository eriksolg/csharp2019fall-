using System;
using System.ComponentModel;
using GameEngine;

namespace ConsoleUI
{
    public static class GameUI
    {
        private static readonly string VerticalSeparator = "|";
        private static readonly string HorizontalSeparator = "-";
        private static readonly string CenterSeparator = "+";
        
        public static void PrintBoard(Game game)
        {
            for (var yIndex = -1; yIndex < game.BoardHeight; yIndex++)
            {
                var line = "";
                for (var xIndex = -1; xIndex < game.BoardWidth; xIndex++)
                {
                    if (yIndex == -1 && xIndex == -1)
                    {
                        line = "   ";
                    }
                    else if (yIndex == -1 && xIndex.ToString().Length == 2)
                    {
                        line = line + " " + xIndex + "";
                    }
                    else if (yIndex == -1)
                    {
                        line = line + " " + xIndex + " ";
                    }
                    else
                    {
                        if (xIndex == -1)
                        {
                            if (yIndex.ToString().Length == 2)
                            {
                                line = line + " " + yIndex + "";
                            }
                            else
                            {
                                line = line + " " + yIndex + " ";
                            }
                        }
                        else
                        {
                            line = line + " " + PrintCellState(game, yIndex, xIndex) + " ";
                        }
                    }

                    if (xIndex < game.BoardWidth - 1)
                    {
                        line = line + VerticalSeparator;
                    }
                }
                
                Console.WriteLine(line);

                if (yIndex < game.BoardHeight - 1)
                {
                    line = "";
                    for (var xIndex = 0; xIndex <= game.BoardWidth; xIndex++)
                    {
                        line = line + HorizontalSeparator + HorizontalSeparator + HorizontalSeparator;
                        if (xIndex <= game.BoardWidth - 1)
                        {
                            line = line + CenterSeparator;
                        }
                    }
                    Console.WriteLine(line);
                }
            }

        }

        private static string PrintCellState(Game game, int yIndex, int xIndex)
        {
            if (game.GameStatus == GameStatus.NotStarted)
            {
                return "·";
            }
            
            var board = game.GetBoard();
            var cell = board[yIndex, xIndex];
            
            if (!cell.IsOpened)
            {
                return cell.IsMarked ? "⚑" : "·";
            }

            if (cell.HasBomb)
            {
                return "B";
            }

            var numberOfBombsNearby = game.GetNumberOfBombsNearCell(yIndex, xIndex);
            
            return numberOfBombsNearby != 0 ? numberOfBombsNearby.ToString() : " ";
        }
    }
}