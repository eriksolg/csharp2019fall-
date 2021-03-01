using System;
using System.ComponentModel;
using GameEngine;

namespace ConsoleUI
{
    public class GameUI
    {
        private static readonly string _verticalSeparator = "|";
        private static readonly string _horizontalSeparator = "-";
        private static readonly string _centerSeparator = "+";
        
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
                    } else if (yIndex == -1)
                    {
                        line = line + " " + xIndex + " ";
                    } else if (xIndex == -1)
                    {
                        line = line + " " + yIndex + " ";
                    }
                    else
                    {
                        line = line + " " + PrintCellState(game, yIndex, xIndex) + " ";
                    }
                    if (xIndex < game.BoardWidth - 1)
                    {
                        line = line + _verticalSeparator;
                    }
                }
                
                Console.WriteLine(line);

                if (yIndex < game.BoardHeight - 1)
                {
                    line = "";
                    for (int xIndex = 0; xIndex <= game.BoardWidth; xIndex++)
                    {
                        line = line + _horizontalSeparator + _horizontalSeparator + _horizontalSeparator;
                        if (xIndex <= game.BoardWidth - 1)
                        {
                            line = line + _centerSeparator;
                        }
                    }
                    Console.WriteLine(line);
                }
            }

        }

        private static string PrintCellState(Game game, int yIndex, int xIndex)
        {
            var board = game.GetBoard();
            var cell = board[yIndex, xIndex];
            
            if (!cell.IsOpened)
            {
                return cell.IsMarked ? "⚑" : "·";
            }

            if (cell.HasBomb)
            {
                return "💣";
            }

            var numberOfBombsNearby = Game.GetNumberOfBombsNearCell(board, yIndex, xIndex);
            
            return numberOfBombsNearby != 0 ? numberOfBombsNearby.ToString() : " ";
        }
    }
}