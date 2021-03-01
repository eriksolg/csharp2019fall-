using System;
using System.Dynamic;
using System.Linq;

namespace GameEngine
{
    public class Game
    {
        private Cell[,] Board { get; set; }

        private readonly int _numberOfMines;
        private GameStatus _gameStatus;
        public int BoardWidth { get; }
        public int BoardHeight { get; }

        public GameStatus GameStatus
        {
            get => _gameStatus;
        }

        public Game(Difficulty difficulty = Difficulty.Easy)
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
            
            int GetCellLocation(int cellY, int cellX)
            {
                return cellY * BoardWidth + cellX;
            }
            
            int[] GenerateMineLocations()
            {
                Random rnd = new Random();
                int[] mineLocations = new int[_numberOfMines];
                for (var i = 0; i < _numberOfMines; i++)
                {
                    mineLocations[i] = rnd.Next(0, BoardHeight * BoardWidth - 1);
                }

                return mineLocations;
            }
        }
        
        public Cell[,] GetBoard()
        {
            var result = new Cell[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }
        
        public static int GetNumberOfBombsNearCell(Cell[,] board, int yIndex, int xIndex) {
            var numberOfBombsNearby = 0;
            numberOfBombsNearby += board[yIndex - 1, xIndex - 1].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex - 1, xIndex ].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex - 1, xIndex + 1].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex, xIndex - 1].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex, xIndex + 1].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex + 1, xIndex - 1].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex + 1, xIndex].HasBomb ? 1 : 0;
            numberOfBombsNearby += board[yIndex + 1, xIndex + 1].HasBomb ? 1 : 0;
            return numberOfBombsNearby;
        }

        public void OpenCell(int yIndex, int xIndex)
        {
            Cell cell = Board[yIndex, xIndex];
            cell.IsOpened = true;
            
            if (cell.HasBomb)
            {
                _gameStatus = GameStatus.Lost;
                return;
            }

            if (GetNumberOfBombsNearCell(Board, yIndex, xIndex) == 0)
            {
                OpenCell(yIndex - 1, xIndex - 1);
                OpenCell(yIndex - 1, xIndex);
                OpenCell(yIndex - 1, xIndex + 1);
                OpenCell(yIndex, xIndex - 1);
                OpenCell(yIndex, xIndex + 1);
                OpenCell(yIndex + 1, xIndex - 1);
                OpenCell(yIndex + 1, xIndex);
                OpenCell(yIndex + 1, xIndex + 1);
            }
        }

        public void MarkCell(int yIndex, int xIndex)
        {
            Cell cell = Board[yIndex, xIndex];
            cell.IsMarked = true;
        }
    }
}