using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Domain;

namespace GameEngine
{
    public class Game
    {
        public Cell[][]? Board { get; set; }
        public int _numberOfMines { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public GameStatus GameStatus { get; set; }

        public Game()
        {
            
        }

        public Game(Difficulty difficulty, SavedGame? savedGame = null)
        {
            if (savedGame != null)
            {
                Board = savedGame.Board;
                BoardHeight = savedGame.BoardHeight;
                BoardWidth = savedGame.BoardWidth;
                GameStatus = savedGame.GameStatus;
                return;
            }
            
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }

            Board = new Cell[BoardHeight][];
            for (var i = 0; i < Board.Length; i++)
            {
                Board[i] = new Cell[BoardWidth];
            }
            GameStatus = GameStatus.NotStarted;
        }

        public void HandleFirstMove(int firstMoveY, int firstMoveX)
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
                    if (Board == null)
                        continue;
                    
                    Board[y][x] = new Cell();
                    if (mineLocations.Contains((y, x)))
                    {
                        Board[y][x].HasBomb = true;
                    }
                }
            }

            (int, int)[] GenerateMineLocations()
            {
                var rnd = new Random();
                var generatedMineLocations = new (int, int)[_numberOfMines];
                for (var i = 0; i < _numberOfMines; i++)
                {
                    (int, int) generatedLocation;
                    do
                    {
                        var generatedNumber = rnd.Next(0, BoardHeight * BoardWidth - 1);
                        var x = generatedNumber % BoardWidth;
                        var y = (generatedNumber - x) / BoardWidth;
                        generatedLocation = (y, x);
                    } while (generatedLocation == (firstMoveY, firstMoveX));
                    
                    generatedMineLocations[i] = generatedLocation;
                }

                return generatedMineLocations;
            }
        }
        
        public Cell[][] GetBoard()
        {
            var result = new Cell[BoardHeight][];
            if (Board != null)
                Array.Copy(Board, result, Board.Length);

            if (GameStatus != GameStatus.NotStarted)
            {
                for (var yIndex = 0; yIndex < result.Length; yIndex++)
                {
                    for (var xIndex = 0; xIndex < result[0].Length; xIndex++)
                    {
                        var bombs = GetNumberOfBombsNearCell(yIndex, xIndex);
                        result[yIndex][xIndex].NumberOfBombsNearby = bombs;
                    }
                }
            }

            return result;
        }
        
        public int GetNumberOfBombsNearCell(int yIndex, int xIndex) {

            var numberOfBombsNearby = 0;

            foreach (var (item1, item2) in GetNeighbours(yIndex, xIndex))
            {
                numberOfBombsNearby += Board != null && Board[item1][item2].HasBomb ? 1 : 0;
            }
            return numberOfBombsNearby;
        }

        public void OpenCell(int yIndex, int xIndex)
        {
            
            if (GameStatus == GameStatus.NotStarted)
            {
                HandleFirstMove(yIndex, xIndex);
            }
            
            if (Board != null)
            {
                Cell cell = Board[yIndex][xIndex];
                cell.IsOpened = true;
            }

            if (GetNumberOfBombsNearCell(yIndex, xIndex) != 0)
                return;
            
            foreach (var (item1, item2) in GetNeighbours(yIndex, xIndex))
            {
                if (Board != null && Board[item1][item2].IsOpened)
                {
                    continue;
                }
                OpenCell(item1, item2);
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
                HandleFirstMove(yIndex, xIndex);
            }

            if (Board != null)
            {
                var cell = Board[yIndex][xIndex];
                cell.IsMarked = !cell.IsMarked;
            }
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
                    if (Board == null) continue;
                    
                    Cell cell = Board[y][x];
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
            var result = new List<(int, int)>();
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