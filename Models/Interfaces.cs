using System;

namespace StartupPlatform
{
    // Будь-який об'єкт, який реалізує цей інтерфейс, може приймати гроші
    public interface IInvestable
    {
        void ReceiveInvestment(double amount);

        // Подія, що спрацьовує при повному фінансуванні
        event Action<string> OnFullyFunded;
    }
}