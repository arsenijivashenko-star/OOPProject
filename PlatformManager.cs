using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StartupPlatform
{
    public class PlatformManager
    {
        private List<Project> projects = new List<Project>();
        private ProjectRepository repo;
        private Investor currentInvestor = new Investor("Головний Інвестор", 500000);
        private const string UiFileName = "ui.json";
        private const string LogFileName = "transactions.log";

        public void Start()
        {
            UIManager.Load(UiFileName);
            repo = new ProjectRepository(UIManager.Strings);
            projects = repo.Load();

            // Підписуємо вже завантажені проєкти на подію
            foreach (var p in projects)
            {
                p.OnFullyFunded += HandleProjectFunded;
            }

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

        // Обробник події (Delegate handler)
        private void HandleProjectFunded(string projectName)
        {
            Console.WriteLine(string.Format(UIManager.Strings.ProjectFundedEvent, projectName));
        }

        // Логування транзакцій у файл
        private void LogTransaction(string investorName, double amount, string projectName)
        {
            string logEntry = $"[{DateTime.Now}] {investorName} інвестував {amount}$ у {projectName}.";
            File.AppendAllText(LogFileName, logEntry + Environment.NewLine);
            Console.WriteLine(UIManager.Strings.TransactionLogged);
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

            Project newProject = null;
            if (type == "1") newProject = new Startup(name, req);
            else if (type == "2") newProject = new CharityProject(name, req);
            else { Console.WriteLine(ui.ErrorInvalidType); return; }

            // Підписка на подію для нових проєктів
            newProject.OnFullyFunded += HandleProjectFunded;

            projects.Add(newProject);
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
                    Project selectedProject = projects[index - 1];
                    try
                    {
                        
                        currentInvestor.Invest(selectedProject, amount);

                      
                        Console.WriteLine(string.Format(ui.SuccessInvested, currentInvestor.Name, amount, selectedProject.Name));
                        LogTransaction(currentInvestor.Name, amount, selectedProject.Name);
                    }
                    catch (InsufficientFundsException ex)
                    {
                        
                        Console.WriteLine(string.Format(ui.ErrorPrefix, ex.Message));
                    }
                    catch (Exception ex)
                    {
                        
                        Console.WriteLine(string.Format(ui.ErrorPrefix, $"Невідома помилка: {ex.Message}"));
                    }
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