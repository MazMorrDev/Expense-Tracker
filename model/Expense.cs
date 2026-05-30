using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker;

public class Expense
{
    [Key]
    public required int Id { get; set; }
    public required string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "General";
    public required decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
