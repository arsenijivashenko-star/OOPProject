using System;
using System.IO;
using System.Text.Json;

namespace StartupPlatform
{
    public static class UIManager
    {
        public static UIConfig Strings { get; private set; }

        public static void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                Strings = JsonSerializer.Deserialize<UIConfig>(json);
            }
            else
            {
                Console.WriteLine($"[ПОМИЛКА] Файл {fileName} не знайдено!");
                Environment.Exit(1);
            }
        }
    }