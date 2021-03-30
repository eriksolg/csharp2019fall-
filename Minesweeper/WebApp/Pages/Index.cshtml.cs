using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public void OnGet() {}

        public bool GameIsOnGoing()
        {
            if (HttpContext.Session.GetString("Game") == null)
                return false;
            
            var game = JsonConvert.DeserializeObject<GameEngine.Game>(HttpContext.Session.GetString("Game"));
            return game.GameStatus == GameStatus.InProgress;
        }
        
    }
}