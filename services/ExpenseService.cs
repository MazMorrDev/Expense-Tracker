using TaskTrackerCLI.Services;

namespace ExpenseTracker;

public class ExpenseService
{
    private readonly StorageService _storageService;

    public ExpenseService()
    {
        _storageService = new StorageService();
    }

    public Expense CreateExpense(string description, string category, decimal amount)
    {
        Expense expense = new()
        {
            Id = GetLastIdPlusOne(),
            Amount = amount,
            Description = description,
            Category = category
        };
        List<Expense> expenses = GetAllExpenses();
        expenses.Add(expense);
        _storageService.Save(expenses);
        return expense;
    }

    public List<Expense> GetAllExpenses()
    {
        return _storageService.Load();
    }

    public int GetLastIdPlusOne()
    {
        List<Expense> expenses = GetAllExpenses();
        // Knowed Bug: If a user deletes one intermedial expense
        // then the IDs will be duplicated in the save operation
        return expenses.Count + 1;
    }
}
