using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class UpdateCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    private readonly Option<int> _idOption = new("--id", "-i")
    {
        Required = true,
        Description = "Expense ID"
    };

    private readonly Option<string> _descriptionOption = new("--description", "-d")
    {
        Description = "Expense description"
    };

    private readonly Option<decimal> _amountOption = new("--amount", "-a")
    {
        Description = "Expense amount"
    };

    private readonly Option<string> _categoryOption = new("--category", "-c")
    {
        DefaultValueFactory = _ => "General",
        Description = "Expense category"
    };

    public Command GetUpdateCommand()
    {
        var updateCommand = new Command("update", "Update an existent Expense");

        updateCommand.Options.Add(_idOption);
        updateCommand.Options.Add(_descriptionOption);
        updateCommand.Options.Add(_amountOption);
        updateCommand.Options.Add(_categoryOption);

        updateCommand.SetAction(parseResult =>
        {
            int id = parseResult.GetValue(_idOption);
            string description = parseResult.GetValue(_descriptionOption)!;
            decimal amount = parseResult.GetValue(_amountOption);
            string category = parseResult.GetValue(_categoryOption)!;

            Expense? expense = _expenseService.UpdateExpense(id, description, category, amount);

            if (expense != null)
            {
                Console.WriteLine($"✅ Added Expense:");
                Console.WriteLine($"   ID: {expense.Id}");
                Console.WriteLine($"   Description: {expense.Description}");
                Console.WriteLine($"   Amount: ${expense.Amount}");
                Console.WriteLine($"   Category: {expense.Category}");
                Console.WriteLine($"   Date: {expense.Date}");
            }

            return 0;
        });

        return updateCommand;
    }
}
