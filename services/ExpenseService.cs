using TaskTrackerCLI.Services;

namespace ExpenseTracker;

public class ExpenseService(StorageService storageService)
{
    private readonly StorageService _storageService = storageService;

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

    public void Delete(List<Expense> expenses, int id)
    {
        Expense? expense = expenses.FirstOrDefault(expense => expense.Id == id);
        if (expense == null)
        {
            Console.WriteLine("The Provided ID was not found.");
            return;
        }
        expenses.Remove(expense);
    }
}
