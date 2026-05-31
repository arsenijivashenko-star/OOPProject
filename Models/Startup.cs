using System;

namespace StartupPlatform
{
    public class Startup : Project
    {
        public Startup(string name, double requiredFunding, double currentFunding = 0)
            : base(name, requiredFunding, currentFunding) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[СТАРТАП] {Name} | Потрібно: {RequiredFunding}$ | Зібрано: {CurrentFunding}$");
        }

        public override string ToCsvRow()
        {
            return $"Startup;{Name};{RequiredFunding};{CurrentFunding}";
        }
    }

    public class CharityProject : Project
    {
        public CharityProject(string name, double requiredFunding, double currentFunding = 0)
            : base(name, requiredFunding, currentFunding) { }

        public override void DisplayInfo()
        {
            Console.WriteLine($"[БЛАГОДІЙНІСТЬ] {Name} | Ціль: {RequiredFunding}$ | Зібрано: {CurrentFunding}$");
        }

        public override string ToCsvRow()
        {
            return $"Charity;{Name};{RequiredFunding};{CurrentFunding}";
        }
    }
}