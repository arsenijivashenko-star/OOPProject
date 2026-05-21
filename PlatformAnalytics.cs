namespace StartupPlatform
{
    public static class PlatformAnalytics
    {
        public static int TotalRegisteredStartups { get; private set; } = 0;
        public static double TotalInvestedMoney { get; private set; } = 0;

        public static void RegisterStartup() => TotalRegisteredStartups++;
        public static void RegisterInvestment(double amount) => TotalInvestedMoney += amount;
    }
}