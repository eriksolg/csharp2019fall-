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
        public DbSet<SavedGame> SavedGames { get; set; }
        public void OnGet()
        {


        }

        public IActionResult OnGetLoadGame(int id)
        {
            GetSavedGames();
            SavedGame savedGame = SavedGames.First(sg => sg.SavedGameId == id);
            GameEngine.Game game = new GameEngine.Game(Difficulty.Easy, savedGame);
            HttpContext.Session.SetString("Game", JsonConvert.SerializeObject(game));
            return Redirect("./Game");
        }


        public bool GetSavedGames()
        {
            SavedGames = SavedGameHandler.GetSavedGames();
            if (!SavedGames.Any())
                return false;

            return true;
        }
    }
}