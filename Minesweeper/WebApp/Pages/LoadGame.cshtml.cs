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
    public class LoadGame : PageModel
    {
        public void OnGet() {}

        public IActionResult OnGetLoadGame(int id)
        {
            var savedGame = GetSavedGames().First(sg => sg.SavedGameId == id);
            var game = new GameEngine.Game(Difficulty.Easy, savedGame);
            HttpContext.Session.SetString("Game", JsonConvert.SerializeObject(game));
            return Redirect("./Game");
        }
        
        public DbSet<SavedGame> GetSavedGames()
        {
            return SavedGameHandler.GetSavedGames();
        }
    }
}