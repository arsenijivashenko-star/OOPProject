using System;
using System.Xml.Linq;

namespace StartupPlatform
{
    public partial class Startup
    {
        public void SetPlan(double profit, int months)
        {
            Plan = new FinancialPlan(profit, months);
            Console.WriteLine($"План для {Name} встановлено: окупність {months} міс.");
        }
    }
}