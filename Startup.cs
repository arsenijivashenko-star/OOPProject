namespace StartupPlatform
{
    public partial class Startup
    {
        // Вкладений клас

        public string Name { get; set; }
        public double RequiredFunding { get; set; }
        public double CurrentFunding { get; set; }
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

        public FinancialPlan Plan { get; set; }
    }
}