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
            var board = game.GetBoard();
            for (int yIndex = 0; yIndex < game.BoardHeight; yIndex++)
            {
                var line = "";
                for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                {
                    line = line + " "  + " ";
                    if (xIndex < game.BoardWidth - 1)
                    {
                        line = line + _verticalSeparator;
                    }
                }
                
                Console.WriteLine(line);

                if (yIndex < game.BoardHeight - 1)
                {
                    line = "";
                    for (int xIndex = 0; xIndex < game.BoardWidth; xIndex++)
                    {
                        line = line + _horizontalSeparator + _horizontalSeparator + _horizontalSeparator;
                        if (xIndex < game.BoardWidth - 1)
                        {
                            line = line + _centerSeparator;
                        }
                    }
                    Console.WriteLine(line);
                }
            }

        }
    }
}