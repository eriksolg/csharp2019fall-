using System;
using Domain;
using GameEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages
{
    public class Game : PageModel
    {
        [FromQuery(Name = "difficulty")]
        public string DifficultyString { get; set; }
        [FromQuery(Name = "newGame")]
        public bool NewGame { get; set; }
        public void OnGet()
        {
            if (NewGame)
                ClearGame();

            if (string.IsNullOrWhiteSpace(DifficultyString))
                return;

            if (!Enum.TryParse<Difficulty>(DifficultyString, true, out var difficulty))
                return;
            
            var game = new GameEngine.Game(difficulty);
            
            SerializeGame(game);

            return;
        }

        public void OnPostHandleMove(int y, int x, bool flagging = false)
        {
            var game = DeSerializeGame();
            
            if (flagging)
                game.MarkCell(y, x);
            else
                game.OpenCell(y, x);
            
            game.UpdateGameStatus();
            
            if (game.GameStatus == GameStatus.Lost || game.GameStatus == GameStatus.Won)
            {
                game.OpenAllCells();
            }
            
            SerializeGame(game);
        }

        public void OnPostClearGame()
        {
            ClearGame();
        }

        public JsonResult OnGetBoard()
        {
            var game = DeSerializeGame();
            var board = game.GetBoard();
            
            return new JsonResult(board);
        }
        
            
        public JsonResult OnGetGameStatus()
        {
            var game = DeSerializeGame();
            

            
            return new JsonResult(game.GameStatus);
        }

        private void ClearGame()
        {
            HttpContext.Session.Remove("Game");
        }

        private GameEngine.Game DeSerializeGame()
        {
            return JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
        }

        private void SerializeGame(GameEngine.Game game)
        {
            HttpContext.Session.SetString("Game", JsonConvert.SerializeObject(game));
        }
    }
}