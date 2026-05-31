using System;

namespace StartupPlatform
{
    public class Startup : Project
    {
        public Startup(string name, double requiredFunding, double currentFunding = 0)
            : base(name, requiredFunding, currentFunding) { }

        public override void DisplayInfo()
        {
            Console.WriteLine(string.Format(UIManager.Strings.StartupInfo, Name, RequiredFunding, CurrentFunding));
        }

        public override string ToCsvRow() => $"Startup;{Name};{RequiredFunding};{CurrentFunding}";
    }

    public class CharityProject : Project
    {
        public CharityProject(string name, double requiredFunding, double currentFunding = 0)
            : base(name, requiredFunding, currentFunding) { }

        public override void DisplayInfo()
        {
            Console.WriteLine(string.Format(UIManager.Strings.CharityInfo, Name, RequiredFunding, CurrentFunding));
        }

        public override string ToCsvRow() => $"Charity;{Name};{RequiredFunding};{CurrentFunding}";
    }
}