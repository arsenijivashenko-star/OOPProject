using System;
using System.Xml.Linq;

namespace StartupPlatform
{
    public partial class Startup
    {
        public Startup(string name, double requiredFunding)
        {
            Name = name;
            RequiredFunding = requiredFunding;
            CurrentFunding = 0;
        }
        public void SetPlan(double profit, int months)
        {
            Plan = new FinancialPlan(profit, months);
            Console.WriteLine($"План для {Name} встановлено: окупність {months} міс.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"- {Name} | Потрібно: {RequiredFunding}$ | Зібрано: {CurrentFunding}$");
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine($"{Name};{RequiredFunding};{CurrentFunding}");
            }
        }
    }
}