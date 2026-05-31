using System;

namespace StartupPlatform
{
    // Startup наслідує базові властивості від Project
    public class Startup : Project
    {
        public FinancialPlan Plan { get; set; }

        public Startup(string name, double requiredFunding, double currentFunding = 0)
            : base(name, requiredFunding, currentFunding)
        {
        }

        public void SetPlan(double profit, int months)
        {
            Plan = new FinancialPlan(profit, months);
            Console.WriteLine($"План для {Name} встановлено: окупність {months} міс.");
        }

        // Реалізація абстрактного методу базового класу
        public override void DisplayInfo()
        {
            Console.WriteLine($"- {Name} | Потрібно: {RequiredFunding}$ | Зібрано: {CurrentFunding}$");
        }
    }

    // Винесено з тіла Startup для чистоти
    public class FinancialPlan
    {
        public double EstimatedProfit { get; set; }
        public int MonthsToBreakEven { get; set; }

        public FinancialPlan(double profit, int months)
        {
            EstimatedProfit = profit;
            MonthsToBreakEven = months;
        }
    }
}