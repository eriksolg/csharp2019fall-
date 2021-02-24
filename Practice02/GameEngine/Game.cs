using System;

namespace GameEngine
{
    /// <summary>
    /// Tic-Tac-Toe
    /// </summary>
    public class Game
    {
        private CellState[,] Board { get; set; }

        public int BoardWidth { get;  }
        public int BoardHeight { get; }

        private bool _playerZeroMove;

        public Game() : this(3, 3)
        {
        }
        
        public Game(int boardHeight, int boardWidth)
        {
            if (boardHeight < 3 || boardHeight < 3)
            {
                throw new ArgumentException("Board size has to be at least 3x3!");
            }
            BoardHeight = boardHeight;
            BoardWidth = boardWidth;
            Board = new CellState[boardHeight, boardWidth];
        }

        public CellState[,] GetBoard()
        {
            var result = new CellState[BoardHeight, BoardWidth];
            Array.Copy(Board, result, Board.Length);
            return result;
        }

        public void Move(int posY, int posX)
        {
            if (Board[posY, posX] != CellState.Empty)
            {
                return;
            }
            
            Board[posY, posX] = _playerZeroMove ? CellState.X : CellState.O;
            _playerZeroMove = !_playerZeroMove;
        }
    }
}