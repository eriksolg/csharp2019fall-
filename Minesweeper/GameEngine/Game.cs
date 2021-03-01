﻿using System;
using System.Collections.Generic;
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
        }

        private void InitializeBoard(int firstMoveY, int firstMoveX)
        {
            _gameStatus = GameStatus.InProgress;
            
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
                    int generatedLocation;
                    
                    do
                    {
                        generatedLocation = rnd.Next(0, BoardHeight * BoardWidth - 1);
                    } while (generatedLocation == GetCellLocation(firstMoveY, firstMoveX));
                    
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
            if (_gameStatus == GameStatus.NotStarted)
            {
                InitializeBoard(yIndex, xIndex);
            }
            Cell cell = Board[yIndex, xIndex];
            cell.IsOpened = true;
            
            if (cell.HasBomb)
            {
                _gameStatus = GameStatus.Lost;
                return;
            }

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
            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    OpenCell(y, x);
                }
            }
        }

        public void MarkCell(int yIndex, int xIndex)
        {
            if (_gameStatus == GameStatus.NotStarted)
            {
                InitializeBoard(yIndex, xIndex);
            }
            
            Cell cell = Board[yIndex, xIndex];
            cell.IsMarked = !cell.IsMarked;
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