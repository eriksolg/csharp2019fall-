using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class Game
    {
        private Cell[,] Board { get; }

        private readonly int _numberOfMines;
        public int BoardWidth { get; }
        public int BoardHeight { get; }
        public GameStatus GameStatus { get; private set; }

        public Game(Difficulty difficulty = Difficulty.Easy)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    BoardHeight = 8;
                    BoardWidth = 8;
                    _numberOfMines = 10;
                    break;
                case Difficulty.Medium:
                    BoardHeight = 16;
                    BoardWidth = 16;
                    _numberOfMines = 40;
                    break;
                case Difficulty.Hard:
                    BoardWidth = 16;
                    BoardHeight = 30;
                    _numberOfMines = 99;
                    break;
            }

            Board = new Cell[BoardHeight, BoardWidth];
            GameStatus = GameStatus.NotStarted;
        }

        private void HandleFirstMove(int firstMoveY, int firstMoveX)
        {
            GameStatus = GameStatus.InProgress;
            InitializeBoard(firstMoveY, firstMoveX);
        }

        private void InitializeBoard(int firstMoveY, int firstMoveX)
        {
            
            var mineLocations = GenerateMineLocations();
            for (var y = 0; y < BoardHeight; y++)
            {
                for (var x = 0; x < BoardWidth; x++)
                {
                    Board[y, x] = new Cell();
                    if (mineLocations.Contains((y, x)))
                    {
                        Board[y, x].HasBomb = true;
                    }
                }
            }

            (int, int)[] GenerateMineLocations()
            {
                Random rnd = new Random();
                (int, int)[] mineLocations = new (int, int)[_numberOfMines];
                for (var i = 0; i < _numberOfMines; i++)
                {
                    (int, int) generatedLocation;
                    do
                    {
                        int generatedNumber = rnd.Next(0, BoardHeight * BoardWidth - 1);
                        int x = generatedNumber % BoardWidth;
                        int y = (generatedNumber - x) / BoardWidth;
                        generatedLocation = (y, x);
                    } while (generatedLocation == (firstMoveY, firstMoveX));
                    
                    mineLocations[i] = generatedLocation;
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
        
        public int GetNumberOfBombsNearCell(int yIndex, int xIndex) {
            var numberOfBombsNearby = 0;

            foreach ((int, int) neighbor in GetNeighbours(yIndex, xIndex))
            {
                numberOfBombsNearby += Board[neighbor.Item1, neighbor.Item2].HasBomb ? 1 : 0;
            }
            return numberOfBombsNearby;
        }

        public void OpenCell(int yIndex, int xIndex)
        {
            if (GameStatus == GameStatus.NotStarted)
            {
                HandleFirstMove(yIndex, xIndex);
            }
            
            Cell cell = Board[yIndex, xIndex];
            cell.IsOpened = true;

            if (GetNumberOfBombsNearCell( yIndex, xIndex) == 0)
            {
                foreach ((int, int) neighbor in GetNeighbours(yIndex, xIndex))
                {
                    if (Board[neighbor.Item1, neighbor.Item2].IsOpened)
                    {
                        continue;
                    }
                    OpenCell(neighbor.Item1, neighbor.Item2);
                }
            }
        }

        public void OpenAllCells()
        {
            for (var y = 0; y < BoardHeight; y++)
            {
                for (var x = 0; x < BoardWidth; x++)
                {
                    OpenCell(y, x);
                }
            }
        }

        public void MarkCell(int yIndex, int xIndex)
        {
            if (GameStatus == GameStatus.NotStarted)
            {
                InitializeBoard(yIndex, xIndex);
            }
            
            Cell cell = Board[yIndex, xIndex];
            cell.IsMarked = !cell.IsMarked;
        }

        public void UpdateGameStatus()
        {
            // Game over, nothing to update
            if (GameStatus == GameStatus.Lost ||
                GameStatus == GameStatus.Won)
            {
                return;
            }

            GameStatus currentGameStatus = GameStatus.Won;
            
            for (var y = 0; y < BoardHeight; y++)
            {
                for (var x = 0; x < BoardWidth; x++)
                {
                    Cell cell = Board[y, x];
                    if (cell.HasBomb && cell.IsOpened)
                    {
                        currentGameStatus = GameStatus.Lost;
                    }
                    
                    if ((cell.HasBomb && !cell.IsMarked ||
                        !cell.HasBomb && !cell.IsOpened ) &&
                        currentGameStatus != GameStatus.Lost)
                    {
                        currentGameStatus = GameStatus.InProgress;
                    }
                }
            }

            GameStatus = currentGameStatus;
        }
        
        private List<(int, int)> GetNeighbours(int yIndex, int xIndex)
        {
            List<(int, int)> result = new List<(int, int)>();
            var yMin = yIndex - 1 < 0 ? yIndex : yIndex - 1;
            var yMax = yIndex + 1 >= BoardHeight ? yIndex : yIndex + 1;
            var xMin = xIndex - 1 < 0 ? xIndex : xIndex - 1;
            var xMax = xIndex + 1 >= BoardWidth ? xIndex : xIndex + 1;
            for (var i = yMin; i <= yMax; i++)
            for (var j = xMin; j <= xMax; j++)
                if (i != yIndex || j != xIndex)
                {
                    result.Add( (i, j) );  
                }

            return result;
        }
    }
}