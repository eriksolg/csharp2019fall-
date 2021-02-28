using System;
using System.Linq;

namespace GameEngine
{
    public class Game
    {
        private Cell[,] Board { get; set; }

        private int _numberOfMines;
        private GameStatus _gameStatus;
        public int BoardWidth { get; }
        public int BoardHeight { get; }

        public Game() : this(Difficulty.Easy)
        {
        }

        public Game(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    BoardHeight = BoardWidth = 8;
                    _numberOfMines = 10;
                    break;
                case Difficulty.Medium:
                    BoardHeight = BoardWidth = 16;
                    _numberOfMines = 40;
                    break;
                case Difficulty.Hard:
                    BoardWidth = 16;
                    BoardHeight = 30;
                    _numberOfMines = 99;
                    break;
            }
            Board = new Cell[BoardHeight, BoardWidth];
            _gameStatus = GameStatus.NotStarted;
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            var mineLocations = GenerateMineLocations();
            for (var y = 0; y < BoardHeight; y++)
            {
                for (var x = 0; x < BoardWidth; x++)
                {
                    Board[y, x] = new Cell();
                    if (mineLocations.Contains(GetCellLocation(y, x)))
                    {
                        Board[y, x].HasBomb = true;
                    }
                }
            }
            
        }

        private int GetCellLocation(int cellY, int cellX)
        {
            return cellY * BoardWidth + cellX;
        }

        private int[] GenerateMineLocations()
        {
            Random rnd = new Random();
            int[] mineLocations = new int[_numberOfMines];
            for (int i = 0; i < _numberOfMines; i++)
            {
                mineLocations[i] = rnd.Next(0, BoardHeight * BoardWidth - 1);
            }

            return mineLocations;
        }

        public Cell[,] GetBoard()
        {
            var result = new Cell[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }
    }
}