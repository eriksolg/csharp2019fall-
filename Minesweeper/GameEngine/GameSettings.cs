namespace GameEngine
{
    public class GameSettings
    {
        public Cell[][]? Board { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }

        public GameStatus GameStatus { get; set; }
    }
}