namespace ExpenseTracker;

public class Expense
{
    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = "General";

    public decimal Amount { get; set; }
}
