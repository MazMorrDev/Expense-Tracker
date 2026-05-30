namespace ExpenseTracker;

public class ExpenseService
{
    public Expense CreateExpense(string description, string category, decimal amount)
    {
        Expense expense = new()
        {
            Id = GetLastIdPlusOne(),
            Amount = amount,
            Description = description,
            Category = category
        };
    }

    public List<Expense> GetAllExpenses()
    {
        
    }

    public int GetLastIdPlusOne()
    {

    }
}
