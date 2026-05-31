using System;

namespace StartupPlatform
{
    public class Investor
    {
        public string Name { get; set; }
        public double Budget { get; set; }

        public Investor(string name, double budget)
        {
            Name = name;
            Budget = budget;
        }

        // Метод приймає базовий клас Project, тому інвестор зможе
        // інвестувати у будь-яких нащадків (Startup, Charity, тощо)
        public void Invest(Project project, double amount)
        {
            if (Budget >= amount)
            {
                Budget -= amount;
                project.CurrentFunding += amount;
                PlatformAnalytics.RegisterInvestment(amount); // Аналітика рахується тут
                Console.WriteLine($"\n[УСПІХ] {Name} інвестував {amount}$ у {project.Name}\n");
            }
            else
            {
                Console.WriteLine("\n[ПОМИЛКА] Недостатньо коштів!\n");
            }
        }
    }
}