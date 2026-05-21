using System;
using System.Collections.Generic;

namespace StartupPlatform
{
    public class PlatformManager
    {
        private List<Startup> startups = new List<Startup>();
        private Investor currentInvestor;

        public void Start()
        {
            // Створюємо тестового інвестора для платформи
            currentInvestor = new Investor("Головний Інвестор", 500000);

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine($"\n=== ПЛАТФОРМА СТАРТАПІВ ===");
                Console.WriteLine($"Ваш баланс: {currentInvestor.Budget}$");
                Console.WriteLine("1. Додати новий стартап");
                Console.WriteLine("2. Переглянути всі стартапи");
                Console.WriteLine("3. Інвестувати в стартап");
                Console.WriteLine("4. Зберегти базу у файл");
                Console.WriteLine("5. Аналітика платформи");

                Console.WriteLine("0. Вийти");
                Console.Write("Оберіть дію: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddStartupMenu(); break;
                    case "2": ShowStartups(); break;
                    case "3": InvestMenu(); break;
                    case "4": SaveAll(); break;
                    case "5":
                        Console.WriteLine($"Всього стартапів: {PlatformAnalytics.TotalRegisteredStartups}");
                        Console.WriteLine($"Загальний обсяг інвестицій: {PlatformAnalytics.TotalInvestedMoney}$");
                        break;
                    case "0": isRunning = false; break;
                    default: Console.WriteLine("Невідома команда."); break;
                }
            }
        }

        private void AddStartupMenu()
        {
            Console.Write("Назва стартапу: ");
            string name = Console.ReadLine();
            Console.Write("Необхідна сума ($): ");
            double req = Convert.ToDouble(Console.ReadLine());

            startups.Add(new Startup(name, req));
            Console.WriteLine("[УСПІХ] Стартап додано!");

            PlatformAnalytics.RegisterStartup();
        }

        private void ShowStartups()
        {
            Console.WriteLine("\n--- Список стартапів ---");
            for (int i = 0; i < startups.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                startups[i].DisplayInfo();
            }
        }

        private void InvestMenu()
        {
            ShowStartups();
            Console.Write("Введіть номер стартапу для інвестиції: ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;


            if (index >= 0 && index < startups.Count)
            {
                Console.Write("Сума інвестиції ($): ");
                double amount = Convert.ToDouble(Console.ReadLine());
                currentInvestor.Invest(startups[index], amount);
                PlatformAnalytics.RegisterInvestment(amount);
            }
        }

        private void SaveAll()
        {
            foreach (var s in startups) s.SaveToFile("db.txt");
            Console.WriteLine("[УСПІХ] Дані збережено в db.txt");
        }
    }
}