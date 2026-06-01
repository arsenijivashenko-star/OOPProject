using System;

namespace StartupPlatform
{
    public static class ConsoleHelper
    {
        // Зчитує рядок із попереднім виведенням підказки
        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        // Безпечно зчитує double
        public static bool TryReadDouble(string prompt, out double value)
        {
            Console.Write(prompt);
            return double.TryParse(Console.ReadLine(), out value);
        }

        // Безпечно зчитує int
        public static bool TryReadInt(string prompt, out int value)
        {
            Console.Write(prompt);
            return int.TryParse(Console.ReadLine(), out value);
        }
    }
}