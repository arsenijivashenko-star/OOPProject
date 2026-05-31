using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StartupPlatform
{
    public class PlatformManager
    {
        private List<Project> projects = new List<Project>();
        private Investor currentInvestor;

        private const string DbFileName = "db.csv";
        private const string UiFileName = "ui.json";

        public void Start()
        {
            // Завантажуємо всі рядки з файлу ДО того, як виведемо будь-який текст
            UIManager.Load(UiFileName);
            LoadDatabase();

            currentInvestor = new Investor("Головний Інвестор", 500000);
            bool isRunning = true;
            var ui = UIManager.Strings;

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

        private void LoadDatabase()
        {
            if (File.Exists(DbFileName))
            {
                string[] lines = File.ReadAllLines(DbFileName);
                for (int i = 1; i < lines.Length; i++) // Пропуск заголовка
                {
                    var parts = lines[i].Split(';');
                    if (parts.Length == 4)
                    {
                        string type = parts[0];
                        string name = parts[1];
                        double req = Convert.ToDouble(parts[2]);
                        double cur = Convert.ToDouble(parts[3]);

                        if (type == "Startup") projects.Add(new Startup(name, req, cur));
                        else if (type == "Charity") projects.Add(new CharityProject(name, req, cur));
                    }
                }
                Console.WriteLine(string.Format(UIManager.Strings.SystemLoaded, projects.Count, DbFileName));
            }
        }

        private void SaveDatabase()
        {
            using (StreamWriter sw = new StreamWriter(DbFileName, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("Type;Name;RequiredFunding;CurrentFunding");
                foreach (var p in projects)
                {
                    sw.WriteLine(p.ToCsvRow());
                }
            }
            Console.WriteLine(string.Format(UIManager.Strings.SuccessSaved, DbFileName));
        }

        private void AddProjectMenu()
        {
            var ui = UIManager.Strings;
            foreach (var item in ui.AddProjectMenu) Console.WriteLine(item);

            Console.Write(ui.SelectTypePrompt);
            string type = Console.ReadLine();

            Console.Write(ui.NamePrompt);
            string name = Console.ReadLine();

            Console.Write(ui.AmountPrompt);
            if (double.TryParse(Console.ReadLine(), out double req))
            {
                if (type == "1") projects.Add(new Startup(name, req));
                else if (type == "2") projects.Add(new CharityProject(name, req));
                else
                {
                    Console.WriteLine(ui.ErrorInvalidType);
                    return;
                }

                Console.WriteLine(ui.SuccessProjectAdded);
            }
            else
            {
                Console.WriteLine(ui.ErrorInvalidAmount);
            }
        }

        private void ShowProjects()
        {
            var ui = UIManager.Strings;
            Console.WriteLine(ui.ProjectsListTitle);

            if (projects.Count == 0)
            {
                Console.WriteLine(ui.NoProjects);
                return;
            }

            for (int i = 0; i < projects.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                projects[i].DisplayInfo();
            }
        }

        private void InvestMenu()
        {
            ShowProjects();
            if (projects.Count == 0) return;

            var ui = UIManager.Strings;
            Console.Write(ui.InvestProjectNumberPrompt);

            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= projects.Count)
            {
                Console.Write(ui.InvestAmountPrompt);
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    currentInvestor.Invest(projects[index - 1], amount);
                }
                else
                {
                    Console.WriteLine(ui.ErrorInvalidAmount);
                }
            }
            else
            {
                Console.WriteLine(ui.ErrorInvalidProjectNumber);
            }
        }

        private void ShowAnalytics()
        {
            var ui = UIManager.Strings;
            int totalProjects = projects.Count;
            double totalInvested = projects.Sum(p => p.CurrentFunding);
            int fullyFunded = projects.Count(p => p.CurrentFunding >= p.RequiredFunding);

            Console.WriteLine(ui.AnalyticsTitle);
            Console.WriteLine(string.Format(ui.TotalProjects, totalProjects));
            Console.WriteLine(string.Format(ui.TotalInvested, totalInvested));
            Console.WriteLine(string.Format(ui.FullyFunded, fullyFunded));
        }
    }
}