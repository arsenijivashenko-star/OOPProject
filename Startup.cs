using System;
using System.IO;

namespace StartupPlatform
{
    public class Startup
    {
        private string name;
        private double requiredFunding;
        private double currentFunding;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double RequiredFunding
        {
            get { return requiredFunding; }
            set { requiredFunding = value; }
        }
        public double CurrentFunding
        {
            get { return currentFunding; }
            set { currentFunding = value; }
        }

        public Startup() { }

        public Startup(string name, double requiredFunding)
        {
            Name = name;
            RequiredFunding = requiredFunding;
            CurrentFunding = 0;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Стартап: {Name}, Потрібно: {RequiredFunding}$, Зібрано: {CurrentFunding}$");
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine($"Startup: {Name} | Required: {RequiredFunding} | Current: {CurrentFunding}");
            }
        }
    }
}