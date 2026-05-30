using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker;

public class Expense
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public decimal Amount { get; set; }
}
