using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace GameEngine
{
    public class SavedGameHandler
    {
        public static DbSet<SavedGame> GetSavedGames()
        {
            var ctx = new AppDbContext();
            return ctx.SavedGames;
        }

        public static void SaveGame(Game game, SavedGame existingSavedGame = null!)
        {
            var savedGame = new SavedGame()
            {
                DateTime = DateTime.Now,
                Board = game.GetBoard(),
                BoardHeight = game.BoardHeight,
                BoardWidth = game.BoardWidth,
                GameStatus = game.GameStatus
            };
            using var ctx = new AppDbContext();
            
            if (existingSavedGame != null)
            {
                existingSavedGame.Board = savedGame.Board;
                existingSavedGame.BoardHeight = savedGame.BoardHeight;
                existingSavedGame.BoardWidth = savedGame.BoardWidth;
                existingSavedGame.DateTime = savedGame.DateTime;
                ctx.SavedGames.Update(existingSavedGame);
            }
            else
            {
                ctx.SavedGames.Add(savedGame);
            }
            
            ctx.SaveChanges();
        }
    }
}