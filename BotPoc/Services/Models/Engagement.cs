namespace BotPoc.Services
{
    public class Engagement
    {
        public EngagementType Type { get; set; }
    }

    public enum EngagementType
    {
        Payments,
        Savings,
        Loans,
        Betalkoll,
        Cards
    }
}