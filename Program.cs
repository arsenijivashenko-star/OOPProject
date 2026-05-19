using System.Text;

public static class Program
{
    public static void Task1()
    {
        Console.WriteLine("Іващенко Арсеній ІПЗ-11(02) 17 років arsenijivashenko@knu.ua");
        Console.WriteLine("Введіть сторони трикутників та їх градусну міру між сторонами\n А1, B1, A2, B2, кут A1 B1, кут A2 B2");

        //Changed to TryParse cuz copy pasting try catch is bloating and annoying
        string A1 = Console.ReadLine();
        string B1 = Console.ReadLine();
        string A2 = Console.ReadLine();
        string B2 = Console.ReadLine();
        string Angle1 = Console.ReadLine();
        string Angle2 = Console.ReadLine();

        if (A1 == A2 && B1 == B2 && Angle1 == Angle2)
        {
            Console.WriteLine("Трикутники рівні між собою");
        }
        else
        {
            Console.WriteLine("Трикутники не рівні між собою");
        }
    }

    public static void Task2()
    {
        Console.WriteLine("Введіть а та б:");

        double a = 0;
        double b = 0;

        try
        {
            a = Convert.ToDouble(Console.ReadLine());
        }
        catch
        {
            a = 0;
        }

        try
        {
            b = Convert.ToDouble(Console.ReadLine());
        }
        catch
        {
            b = 0;
        }

        double x = (Math.Log10(a) + ((2 + a) / Math.Pow(b, 3))) / (Math.Tan(a) - (1 / Math.Sqrt((a * a) + (b * b))));

        Console.WriteLine("Значення Х: " + Convert.ToString(x));
    }

    public static void Task3()
    {
        Console.WriteLine("Введіть Х");

        double x = 0;

        try
        {
            x = Convert.ToDouble(Console.ReadLine());
        }
        catch
        {
            x = 0;
        }

        if (x < 0 && x < Math.PI)
        {
            x = Math.Sin(x);
        }
        else if (Math.PI < x && x < (Math.PI * 3) / 2)
        {
            x = Math.Cos(x);
        }
        else if ((Math.PI * 3) / 2 < x && x < Math.Tau)
        {
            x = Math.Sin(x) / Math.Cos(x);
        }

        Console.WriteLine("Значення функції f(x): " + Convert.ToString(x));
    }

    public static void Task4()
    {
        Console.WriteLine("Введіть країну на англійській мові:");

        string name = (Console.ReadLine() ?? "").ToLower();

        switch (name)
        {
            case "china":
                Console.WriteLine("1. 578.7");
                break;
            case "singapore":
                Console.WriteLine("2. 556.3");
                break;
            case "macao":
                Console.WriteLine("3. 542.3");
                break;
            case "hong kong":
                Console.WriteLine("4. 530.7");
                break;
            case "estonia":
                Console.WriteLine("5. 525.3");
                break;
            default:
                Console.WriteLine("Не в списку");
                break;
        }
    }

    public static void Task5()
    {
        Console.WriteLine("Введіть натуральне число:");

        long n = 2;

        try
        {
            n = Convert.ToInt64(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Неправильно введене число, використовуєм два.");
        }

        Console.WriteLine("Введіть дійсне число х > 0:");

        double x = 1;

        try
        {
            x = Math.Abs(Convert.ToDouble(Console.ReadLine()));
        }
        catch
        {
            Console.WriteLine("Неправильно введене число, використовуєм один.");
        }

        double comp = 0;

        for (long i = 1; i <= n; ++i)
        {
            comp += (Math.Pow(-1, (i * 2) - 1) * Math.Pow(x, i)) / Math.Pow(1 + x, i);
        }

        Console.WriteLine("Cума членів ряду: " + Convert.ToString(comp));
    }
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8; // fix for ukrainian output
        Console.WriteLine("Іващенко Арсеній Варіант 8");

        byte option = 0;
        do
        {
            Console.WriteLine("1. Task1\n2. Task2\n3. Task3\n4. Task4\n5. Task5\n0. Exit");

            try
            {
                option = Convert.ToByte(Console.ReadLine());
            }
            catch
            {
                option = 0;
            }

            switch (option)
            {
                case 1:
                    Task1();
                    break;
                case 2:
                    Task2();
                    break;
                case 3:
                    Task3();
                    break;
                case 4:
                    Task4();
                    break;
                case 5:
                    Task5();
                    break;
                default:
                    break;
            }

        } while (option != 0);
    }
}