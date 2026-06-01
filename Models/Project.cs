using System;

namespace StartupPlatform
{
    public abstract class Project : IInvestable
    {
        public string Name { get; set; }
        public double RequiredFunding { get; set; }
        public double CurrentFunding { get; set; }

        // Оголошення події з інтерфейсу
        public event Action<string> OnFullyFunded;

        protected Project(string name, double requiredFunding, double currentFunding = 0)
        {
            Name = name;
            RequiredFunding = requiredFunding;
            CurrentFunding = currentFunding;
        }

        // Метод отримання інвестиції
        public void ReceiveInvestment(double amount)
        {
            CurrentFunding += amount;

            // Якщо ціль досягнуто — викликаємо подію (Invoke)
            if (CurrentFunding >= RequiredFunding)
            {
                OnFullyFunded?.Invoke(Name);
            }
        }

        public abstract void DisplayInfo();
        public abstract string ToCsvRow();
    }
}