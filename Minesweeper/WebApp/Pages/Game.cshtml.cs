using System;
using System.Text.Json.Serialization;
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
        
        public void OnPostOpenAllCells()
        {
            game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            game.OpenAllCells();
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
            return new JsonResult(board);
        }
        
        public JsonResult OnGetNumberOfBombsNearCell(int y, int x)
        {
            game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            var bombs = game.GetNumberOfBombsNearCell(y, x);
            return new JsonResult(bombs);
        }
            
        public JsonResult OnGetGameStatus()
        {
            game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            return new JsonResult(game.GameStatus);
        }
    }
}