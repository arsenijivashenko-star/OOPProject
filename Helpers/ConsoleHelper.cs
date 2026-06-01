using System;
using System.Globalization;

namespace StartupPlatform; // Використання file-scoped namespace

public static class ConsoleHelper
{
    public static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    public static bool TryReadDouble(string prompt, out double value)
    {
        Console.Write(prompt);
        string input = Console.ReadLine()?.Replace(',', '.') ?? string.Empty; // Заміна коми на крапку

        return double.TryParse(
            input,
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out value);
    }

    public static bool TryReadInt(string prompt, out int value)
    {
        Console.Write(prompt);
        return int.TryParse(Console.ReadLine(), out value);
    }
}