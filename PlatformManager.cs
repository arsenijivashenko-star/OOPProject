using System;
using System.Collections.Generic;
using System.Linq;

namespace StartupPlatform
{
    public class PlatformManager
    {
        private List<Project> projects = new List<Project>();
        private ProjectRepository repo = new ProjectRepository();
        private Investor currentInvestor = new Investor("Головний Інвестор", 500000);
        private const string UiFileName = "ui.json";

        public void Start()
        {
            UIManager.Load(UiFileName);
            projects = repo.Load();

            var ui = UIManager.Strings;
            while (true)
            {
                Console.WriteLine(ui.Title);
                Console.WriteLine(string.Format(ui.Balance, currentInvestor.Budget));
                ui.MenuItems.ForEach(Console.WriteLine);

                string choice = ConsoleHelper.ReadString(ui.Prompt);
                if (choice == "0") { Console.WriteLine(ui.ExitMessage); break; }

                ExecuteChoice(choice);
            }
        }

        private void ExecuteChoice(string choice)
        {
            switch (choice)
            {
                case "1": AddProjectMenu(); break;
                case "2": ShowProjects(); break;
                case "3": InvestMenu(); break;
                case "4": repo.Save(projects); break;
                case "5": ShowAnalytics(); break;
                default: Console.WriteLine(UIManager.Strings.UnknownCommand); break;
            }
        }

        private void AddProjectMenu()
        {
            var ui = UIManager.Strings;
            ui.AddProjectMenu.ForEach(Console.WriteLine);

            string type = ConsoleHelper.ReadString(ui.SelectTypePrompt);
            string name = ConsoleHelper.ReadString(ui.NamePrompt);

            if (!ConsoleHelper.TryReadDouble(ui.AmountPrompt, out double req))
            {
                Console.WriteLine(ui.ErrorInvalidAmount);
                return;
            }

            if (type == "1") projects.Add(new Startup(name, req));
            else if (type == "2") projects.Add(new CharityProject(name, req));
            else { Console.WriteLine(ui.ErrorInvalidType); return; }

            Console.WriteLine(ui.SuccessProjectAdded);
        }

        private void ShowProjects()
        {
            var ui = UIManager.Strings;
            Console.WriteLine(ui.ProjectsListTitle);

            if (!projects.Any()) { Console.WriteLine(ui.NoProjects); return; }

            for (int i = 0; i < projects.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                projects[i].DisplayInfo();
            }
        }

        private void InvestMenu()
        {
            ShowProjects();
            if (!projects.Any()) return;

            var ui = UIManager.Strings;
            if (ConsoleHelper.TryReadInt(ui.InvestProjectNumberPrompt, out int index) && index > 0 && index <= projects.Count)
            {
                if (ConsoleHelper.TryReadDouble(ui.InvestAmountPrompt, out double amount))
                {
                    currentInvestor.Invest(projects[index - 1], amount);
                }
                else Console.WriteLine(ui.ErrorInvalidAmount);
            }
            else Console.WriteLine(ui.ErrorInvalidProjectNumber);
        }

        private void ShowAnalytics()
        {
            var ui = UIManager.Strings;
            Console.WriteLine(ui.AnalyticsTitle);
            Console.WriteLine(string.Format(ui.TotalProjects, projects.Count));
            Console.WriteLine(string.Format(ui.TotalInvested, projects.Sum(p => p.CurrentFunding)));
            Console.WriteLine(string.Format(ui.FullyFunded, projects.Count(p => p.CurrentFunding >= p.RequiredFunding)));
        }
    }
}