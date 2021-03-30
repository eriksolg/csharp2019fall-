using System;
using System.Linq;
using Domain;
using GameEngine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApp.Pages
{
    public class SaveGame : PageModel
    {
        [FromQuery(Name = "id")]
        public int? SavedGameId { get; set;  }
        
        public void OnGet() {}

        public IActionResult OnGetSaveGame()
        {
            var game = DeSerializeGame();
            if (SavedGameId == null)
            {
                SavedGameHandler.SaveGame(game);
            } else
            {
                var existingSavedGame = GetSavedGames().First(sg => sg.SavedGameId == SavedGameId);
                SavedGameHandler.SaveGame(game, existingSavedGame);
            }
            
            return Redirect("./Game");
        }

        public bool GameIsOnGoing()
        {
            if (HttpContext.Session.GetString("Game") == null)
                return false;

            var game = DeSerializeGame();
            return game.GameStatus == GameStatus.InProgress;
        }

        public DbSet<SavedGame> GetSavedGames()
        {
            return SavedGameHandler.GetSavedGames();
        }
        
        private GameEngine.Game DeSerializeGame()
        {
            return JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
        }
    }
}