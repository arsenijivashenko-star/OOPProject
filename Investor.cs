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
                // Текст витягується з конфігурації
                Console.WriteLine(string.Format(UIManager.Strings.SuccessInvested, Name, amount, project.Name));
            }
            else
            {
                Console.WriteLine(UIManager.Strings.ErrorInsufficientFunds);
            }
        }
    }
}