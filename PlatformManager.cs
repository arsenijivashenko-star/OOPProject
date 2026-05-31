using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace StartupPlatform
{
    public class PlatformManager
    {
        private List<Startup> startups = new List<Startup>();
        private Investor currentInvestor;
        private UIConfig ui;
        private const string DbFileName = "db.txt";
        private const string UiFileName = "ui.json";

        public void Start()
        {
            LoadUIStrings();
            LoadDatabase();

            currentInvestor = new Investor("Головний Інвестор", 500000);
            bool isRunning = true;

            while (isRunning)
            {
                // ВИВЕДЕННЯ МЕНЮ З JSON (через цикл)
                Console.WriteLine(ui.Title);
                Console.WriteLine(string.Format(ui.Balance, currentInvestor.Budget));

                foreach (var item in ui.MenuItems)
                {
                    Console.WriteLine(item);
                }

                Console.Write(ui.Prompt);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStartupMenu(); break;
                    case "2": ShowStartups(); break;
                    case "3": InvestMenu(); break;
                    case "4": SaveDatabase(); break;
                    case "5": ShowAnalytics(); break;
                    case "0":
                        isRunning = false;
                        Console.WriteLine(ui.ExitMessage);
                        break;
                    default:
                        Console.WriteLine(ui.UnknownCommand);
                        break;
                }
            }
        }

        private void LoadUIStrings()
        {
            if (!File.Exists(UiFileName))
            {
                Console.WriteLine($"[КРИТИЧНА ПОМИЛКА] Файл {UiFileName} не знайдено!");
                Environment.Exit(1);
            }

            string json = File.ReadAllText(UiFileName);
            ui = JsonSerializer.Deserialize<UIConfig>(json);
        }

        private void LoadDatabase()
        {
            if (File.Exists(DbFileName))
            {
                string[] lines = File.ReadAllLines(DbFileName);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 3)
                    {
                        string name = parts[0];
                        double req = Convert.ToDouble(parts[1]);
                        double cur = Convert.ToDouble(parts[2]);

                        startups.Add(new Startup(name, req, cur));
                        PlatformAnalytics.RegisterStartup();
                        PlatformAnalytics.RegisterInvestment(cur);
                    }
                }
                Console.WriteLine($"[СИСТЕМА] Завантажено {startups.Count} стартапів з бази даних.");
            }
        }

        private void SaveDatabase()
        {
            // Перезаписуємо файл, щоб оновити поточний стан
            using (StreamWriter sw = new StreamWriter(DbFileName, false))
            {
                foreach (var s in startups)
                {
                    sw.WriteLine($"{s.Name};{s.RequiredFunding};{s.CurrentFunding}");
                }
            }
            Console.WriteLine($"\n[УСПІХ] Базу даних збережено у {DbFileName}\n");
        }

        private void AddStartupMenu()
        {
            Console.Write("Назва стартапу: ");
            string name = Console.ReadLine();
            Console.Write("Необхідна сума ($): ");

            if (double.TryParse(Console.ReadLine(), out double req))
            {
                startups.Add(new Startup(name, req));
                Console.WriteLine("[УСПІХ] Стартап додано!");
                PlatformAnalytics.RegisterStartup();
            }
            else
            {
                Console.WriteLine("[ПОМИЛКА] Некоректна сума!");
            }
        }

        private void ShowStartups()
        {
            Console.WriteLine("\n--- Список стартапів ---");
            if (startups.Count == 0) Console.WriteLine("Стартапів поки немає.");

            for (int i = 0; i < startups.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                startups[i].DisplayInfo();
            }
        }

        private void InvestMenu()
        {
            ShowStartups();
            if (startups.Count == 0) return;

            Console.Write("Введіть номер стартапу для інвестиції: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= startups.Count)
            {
                Console.Write("Сума інвестиції ($): ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    // Індексація з нуля
                    currentInvestor.Invest(startups[index - 1], amount);
                }
            }
            else
            {
                Console.WriteLine("[ПОМИЛКА] Некоректний номер стартапу!");
            }
        }

        private void ShowAnalytics()
        {
            Console.WriteLine("\n--- АНАЛІТИКА ---");
            Console.WriteLine($"Всього стартапів: {PlatformAnalytics.TotalRegisteredStartups}");
            Console.WriteLine($"Загальний обсяг інвестицій: {PlatformAnalytics.TotalInvestedMoney}$");
        }
    }
}