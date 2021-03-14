using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GameEngine
{
    public static class GameConfigHandler
    {
        
        public static void SaveConfig(GameSettings settings, string fileName)
        {
            using var writer = System.IO.File.CreateText($"{fileName}_savegame.json");
            
            var jsonString = JsonSerializer.Serialize(settings);
            writer.Write(jsonString);
        }

        public static GameSettings LoadConfig(string fileName)
        {
            if (!System.IO.File.Exists($"{fileName}_savegame.json"))
                return null;
            
            var jsonString = System.IO.File.ReadAllText($"{fileName}_savegame.json");
            var res = JsonSerializer.Deserialize<GameSettings>(jsonString);

            return res;

        }

        public static string[] GetSavedGames()
        {
            var fileList = Directory.GetFiles(@".", "*_savegame.json");
            for (var i = 0; i < fileList.Length; i++)
            {
                var fileName = fileList[i];
                var fileSuffixPos = fileName.IndexOf("_savegame.json", StringComparison.Ordinal);
                fileList[i] = fileName.Substring(0, fileSuffixPos);
            }

            return fileList;
        }
    }
}