namespace StartupPlatform
{
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштування кодування для коректного відображення української мови в консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            PlatformManager platform = new PlatformManager();
            platform.Start();
        }
    }
}