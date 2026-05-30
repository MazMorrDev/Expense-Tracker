namespace ExpenseTracker.services;

public class ExpenseService(StorageService storageService)
{
    private readonly StorageService _storageService = storageService;

    public Expense CreateExpense(string description, string? category, decimal amount)
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
        if (expenses.Count == 0) return 1;
        int result = expenses.Last().Id + 1;
        return result;
    }

    public void DeleteExpense(int id)
    {
        List<Expense> expenses = GetAllExpenses();
        Expense? expense = expenses.FirstOrDefault(expense => expense.Id == id);
        if (expense == null)
        {
            Console.WriteLine($"The Provided ID: {id} was not found.");
            return;
        }

        expenses.Remove(expense);
        _storageService.Save(expenses);
        Console.WriteLine($"Expense with Id: {id} was sucessfully deleted");
    }
}
