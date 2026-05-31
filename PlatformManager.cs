using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace StartupPlatform
{
    public class PlatformManager
    {
        private List<Project> projects = new List<Project>(); // Тепер тут базовий тип Project!
        private Investor currentInvestor;
        private UIConfig ui;

        private const string DbFileName = "db.csv"; // Формат для Excel
        private const string UiFileName = "ui.json";

        public void Start()
        {
            LoadUIStrings();
            LoadDatabase();

            currentInvestor = new Investor("Головний Інвестор", 500000);
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine(ui.Title);
                Console.WriteLine(string.Format(ui.Balance, currentInvestor.Budget));

                foreach (var item in ui.MenuItems) Console.WriteLine(item);

                Console.Write(ui.Prompt);
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProjectMenu(); break;
                    case "2": ShowProjects(); break;
                    case "3": InvestMenu(); break;
                    case "4": SaveDatabase(); break;
                    case "5": ShowAnalytics(); break;
                    case "0": isRunning = false; Console.WriteLine(ui.ExitMessage); break;
                    default: Console.WriteLine(ui.UnknownCommand); break;
                }
            }
        }

        private void LoadUIStrings()
        {
            if (File.Exists(UiFileName))
            {
                string json = File.ReadAllText(UiFileName);
                ui = JsonSerializer.Deserialize<UIConfig>(json);
            }
            else
            {
                Console.WriteLine($"[ПОМИЛКА] {UiFileName} не знайдено!");
                Environment.Exit(1);
            }
        }

        private void LoadDatabase()
        {
            if (File.Exists(DbFileName))
            {
                string[] lines = File.ReadAllLines(DbFileName);
                for (int i = 1; i < lines.Length; i++) // Починаємо з 1, щоб пропустити заголовок CSV
                {
                    var parts = lines[i].Split(';');
                    if (parts.Length == 4)
                    {
                        string type = parts[0];
                        string name = parts[1];
                        double req = Convert.ToDouble(parts[2]);
                        double cur = Convert.ToDouble(parts[3]);

                        // Відновлюємо правильний тип об'єкта
                        if (type == "Startup") projects.Add(new Startup(name, req, cur));
                        else if (type == "Charity") projects.Add(new CharityProject(name, req, cur));
                    }
                }
                Console.WriteLine($"[СИСТЕМА] Завантажено {projects.Count} проєктів з {DbFileName}.");
            }
        }

        private void SaveDatabase()
        {
            // Зберігаємо у CSV з BOM (щоб Excel коректно читав українські літери)
            using (StreamWriter sw = new StreamWriter(DbFileName, false, System.Text.Encoding.UTF8))
            {
                // Заголовок для Excel
                sw.WriteLine("Type;Name;RequiredFunding;CurrentFunding");
                foreach (var p in projects)
                {
                    sw.WriteLine(p.ToCsvRow());
                }
            }
            Console.WriteLine($"\n[УСПІХ] Базу даних збережено у {DbFileName} (Можна відкрити в Excel)\n");
        }

        private void AddProjectMenu()
        {
            Console.WriteLine("1. Комерційний стартап");
            Console.WriteLine("2. Благодійний проєкт");
            Console.Write("Оберіть тип: ");
            string type = Console.ReadLine();

            Console.Write("Назва: ");
            string name = Console.ReadLine();
            Console.Write("Необхідна сума ($): ");

            if (double.TryParse(Console.ReadLine(), out double req))
            {
                if (type == "1") projects.Add(new Startup(name, req));
                else if (type == "2") projects.Add(new CharityProject(name, req));
                else Console.WriteLine("[ПОМИЛКА] Некоректний тип!");

                Console.WriteLine("[УСПІХ] Проєкт додано!");
            }
        }

        private void ShowProjects()
        {
            Console.WriteLine("\n--- Список проєктів ---");
            if (projects.Count == 0) Console.WriteLine("Проєктів поки немає.");

            for (int i = 0; i < projects.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                projects[i].DisplayInfo(); // Завдяки поліморфізму викличеться правильний метод
            }
        }

        private void InvestMenu()
        {
            ShowProjects();
            if (projects.Count == 0) return;

            Console.Write("Введіть номер проєкту: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= projects.Count)
            {
                Console.Write("Сума інвестиції ($): ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    currentInvestor.Invest(projects[index - 1], amount);
                }
            }
        }

        private void ShowAnalytics()
        {
            // Використання LINQ для аналітики
            int totalProjects = projects.Count;
            double totalInvested = projects.Sum(p => p.CurrentFunding);
            int fullyFunded = projects.Count(p => p.CurrentFunding >= p.RequiredFunding);

            Console.WriteLine("\n--- АНАЛІТИКА (LINQ) ---");
            Console.WriteLine($"Всього проєктів: {totalProjects}");
            Console.WriteLine($"Загальний обсяг інвестицій: {totalInvested}$");
            Console.WriteLine($"Успішно профінансованих проєктів: {fullyFunded}");
        }
    }
}