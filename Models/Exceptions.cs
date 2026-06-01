using System;

namespace StartupPlatform
{
    // Власний клас винятку для нестачі коштів
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message) { }
    }
}