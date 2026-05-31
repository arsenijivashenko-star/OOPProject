using System.Collections.Generic;

namespace StartupPlatform
{
    public class UIConfig
    {
        public string Title { get; set; }
        public string Balance { get; set; }
        public List<string> MenuItems { get; set; }
        public string Prompt { get; set; }
        public string UnknownCommand { get; set; }
        public string ExitMessage { get; set; }
    }
}