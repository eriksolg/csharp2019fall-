using System;

namespace GameEngine
{
    public class Cell
    {
        private Boolean _isOpened;
        private bool _hasBomb;

        public Boolean HasBomb
        {
            get => _hasBomb;
            set
            {
                if (!_isOpened)
                {
                    _hasBomb = value;
                }
            }
        }

        public Cell()
        {
            _isOpened = false;
            _hasBomb = false;
        }
    }
        
}