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

        public void Invest(Project project, double amount)
        {
            if (Budget < amount)
            {
                // Викидаємо власний виняток замість звичайного тексту
                throw new InsufficientFundsException(UIManager.Strings.ErrorInsufficientFunds);
            }

            Budget -= amount;
            project.ReceiveInvestment(amount); // Передаємо гроші в проєкт
        }
    }
}