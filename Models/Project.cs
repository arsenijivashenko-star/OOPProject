namespace StartupPlatform
{
    public abstract class Project
    {
        public string Name { get; set; }
        public double RequiredFunding { get; set; }
        public double CurrentFunding { get; set; }

        protected Project(string name, double requiredFunding, double currentFunding = 0)
        {
            Name = name;
            RequiredFunding = requiredFunding;
            CurrentFunding = currentFunding;
        }

        // Абстрактний метод, який кожен спадкоємець має реалізувати по-своєму
        public abstract void DisplayInfo();

        // Метод для генерації рядка CSV (для Excel)
        public abstract string ToCsvRow();
    }
}