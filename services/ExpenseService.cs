using System.Collections;

namespace ExpenseTracker.services;

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

    public Expense? UpdateExpense(int id, string description, string category, decimal amount)
    {
        Expense? expense = GetExpenseById(id);
        if (expense != null)
        {
            expense.Amount = amount;
            expense.Description = description;
            expense.Category = category;

            DeleteExpense(id);

            List<Expense> expenses = GetAllExpenses();
            expenses.Add(expense);

            _storageService.Save(expenses);
            return expense;
        }
        return null;
    }

    public Expense? GetExpenseById(int id)
    {
        List<Expense> expenses = GetAllExpenses();
        Expense? expense = expenses.FirstOrDefault(expense => expense.Id == id);
        if (expense == null)
        {
            Console.WriteLine($"The Expense with the given ID: {id} was not found");
            return null;
        }
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
        Expense? expense = GetExpenseById(id);
        if (expense == null)
        {
            return;
        }

        expenses.Remove(expense);
        _storageService.Save(expenses);
        Console.WriteLine($"Expense with Id: {id} was sucessfully deleted");
    }
}
