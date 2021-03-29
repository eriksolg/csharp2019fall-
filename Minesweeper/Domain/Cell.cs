using System;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Cell
    {
        private bool _hasBomb;

        public bool HasBomb
        {
            get => _hasBomb;
            set
            {
                if (!IsOpened)
                {
                    _hasBomb = value;
                }
            }
        }
        public bool IsOpened { get; set; }

        public bool IsMarked { get; set; }

        public int NumberOfBombsNearby { get; set; }

        public Cell()
        {
            IsOpened = false;
            _hasBomb = false;
        }
    }
        
}