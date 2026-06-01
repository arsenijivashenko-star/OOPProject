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
        public string SystemLoaded { get; set; }
        public string SuccessSaved { get; set; }
        public List<string> AddProjectMenu { get; set; }
        public string SelectTypePrompt { get; set; }
        public string NamePrompt { get; set; }
        public string AmountPrompt { get; set; }
        public string ErrorInvalidType { get; set; }
        public string ErrorInvalidAmount { get; set; }
        public string SuccessProjectAdded { get; set; }
        public string ProjectsListTitle { get; set; }
        public string NoProjects { get; set; }
        public string InvestProjectNumberPrompt { get; set; }
        public string InvestAmountPrompt { get; set; }
        public string AnalyticsTitle { get; set; }
        public string TotalProjects { get; set; }
        public string TotalInvested { get; set; }
        public string FullyFunded { get; set; }
        public string StartupInfo { get; set; }
        public string CharityInfo { get; set; }
        public string SuccessInvested { get; set; }
        public string ErrorInsufficientFunds { get; set; }
        public string ErrorInvalidProjectNumber { get; set; }
        public string ErrorPrefix { get; set; }
        public string TransactionLogged { get; set; }
        public string ProjectFundedEvent { get; set; }
    }
}