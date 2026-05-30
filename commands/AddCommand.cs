using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker.commands;

public class AddCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    // Definir las opciones una sola vez (como campos)
    private readonly Option<string> _descriptionOption = new("--description", "-d")
    {
        Required = true,
        Description = "Expense description"
    };

    private readonly Option<decimal> _amountOption = new("--amount", "-a")
    {
        Required = true,
        Description = "Expense amount"
    };

    private readonly Option<string> _categoryOption = new("--category", "-c")
    {
        DefaultValueFactory = _ => "General",
        Description = "Expense category"
    };

    public Command GetAddCommand()
    {
        var addCommand = new Command("add", "Add a new Expense");

        // Agregar las mismas instancias
        addCommand.Options.Add(_descriptionOption);
        addCommand.Options.Add(_amountOption);
        addCommand.Options.Add(_categoryOption);

        addCommand.SetAction(parseResult =>
        {
            // Recuperar valores usando las mismas instancias
            string description = parseResult.GetValue(_descriptionOption)!;
            decimal amount = parseResult.GetValue(_amountOption);
            string category = parseResult.GetValue(_categoryOption)!;

            Expense expense = _expenseService.CreateExpense(description, category, amount);

            Console.WriteLine($"✅ Added Expense:");
            Console.WriteLine($"   ID: {expense.Id}");
            Console.WriteLine($"   Description: {expense.Description}");
            Console.WriteLine($"   Amount: ${expense.Amount}");
            Console.WriteLine($"   Category: {expense.Category}");
            Console.WriteLine($"   Date: {expense.Date}");

            return 0;
        });

        return addCommand;
    }
}