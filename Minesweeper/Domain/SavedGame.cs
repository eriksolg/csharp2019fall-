using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Domain
{
    public class SavedGame
    {
        public int SavedGameId { get; set; }
        
        public DateTime DateTime { get; set; }

        [NotMapped]
        public Cell[][]? Board
        {
            get => JsonSerializer.Deserialize<Cell[][]>(BoardData);
            set => BoardData = JsonSerializer.Serialize(value);
        }

        public string BoardData { get; set; } = default!;
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        public GameStatus GameStatus { get; set; }
    }
}