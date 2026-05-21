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

        public void Invest(Startup startup, double amount)
        {
            if (Budget >= amount)
            {
                Budget -= amount;
                startup.CurrentFunding += amount;
                Console.WriteLine($"{Name} інвестував {amount}$ у {startup.Name}");
            }
        }
    }
}