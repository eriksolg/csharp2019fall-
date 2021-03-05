﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello JSON World!");

            var gameSettings = new GameSettings()
            {
                GameName = "testgame",
                BoardHeight = 11,
                BoardWidth = 22
            };

            SaveSettings(gameSettings, "test.json");
            var settings2 = LoadSettings("test.json");

            Console.WriteLine(settings2.GameName);
            static GameSettings LoadSettings(string fileName)
            {
                var jsonString = System.IO.File.ReadAllText(fileName);
                Console.WriteLine(jsonString);
                var res = JsonSerializer.Deserialize<GameSettings>(jsonString);

                return res;
            }
            static void SaveSettings(GameSettings gameSettings, string fileName)
            {
                var jsonOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                var jsonString = JsonSerializer.Serialize<GameSettings>(gameSettings, jsonOptions);
                Console.WriteLine(jsonString);

                using  (var writer = System.IO.File.CreateText(fileName))
                {
                    writer.Write(jsonString);
                }

                // var writer2 = System.IO.File.CreateText("test2.json");
                // writer2.Write(jsonString);
                // writer2.Dispose();
            }
        }
    }

    class GameSettings
    {
        public string GameName { get; set; }
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
    }
}