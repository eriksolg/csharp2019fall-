using System;
using System.Text.Json.Serialization;
using Domain;
using GameEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApp.Pages
{
    public class Game : PageModel
    {
        [FromQuery(Name = "difficulty")]
        public string DifficultyString { get; set; }

        public GameEngine.Game? game { get; set; }
        
        public IActionResult OnGet()
        {
            if (!string.IsNullOrWhiteSpace(DifficultyString))
            {
                Difficulty difficulty;
                if (Enum.TryParse<Difficulty>(DifficultyString, true, out difficulty))
                {
                    Console.WriteLine("Starting game: " + difficulty);
                    game = new GameEngine.Game(difficulty);
                    HttpContext.Session.SetString("Game", JsonConvert.SerializeObject(game));
                }
            }

            return null;
        }

        public void OnPostHandleMove(int y, int x, bool flagging = false)
        {
            game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            if (flagging)
                game.MarkCell(y, x);
            else
                game.OpenCell(y, x);
            
            game.UpdateGameStatus();
            HttpContext.Session.SetString("Game", JsonConvert.SerializeObject(game));
        }

        public void OnPostClearGame()
        {
            HttpContext.Session.Remove("Game");
        }
        
        public JsonResult OnGetBoard()
        {
            game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            var board = game.GetBoard();
            if (game.GameStatus != GameStatus.NotStarted)
            {
                for (var yIndex = 0; yIndex < board.Length; yIndex++)
                {
                    for (var xIndex = 0; xIndex < board[0].Length; xIndex++)
                    {
                        var bombs = game.GetNumberOfBombsNearCell(yIndex, xIndex);
                        board[yIndex][xIndex].NumberOfBombsNearby = bombs;
                    }
                }
            }
            

            return new JsonResult(board);
        }
        
            
        public JsonResult OnGetGameStatus()
        {
            game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            if (game.GameStatus == GameStatus.Lost || game.GameStatus == GameStatus.Won)
            {
                game.OpenAllCells();
                HttpContext.Session.SetString("Game", JsonConvert.SerializeObject(game));
            }
            return new JsonResult(game.GameStatus);
        }
    }
}