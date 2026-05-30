using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker.commands;

public class ListCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    private readonly Option<string> _categoryOption = new("--category", "-c")
    {
        Description = "filter by category"
    };

    public Command GetListCommand()
    {
        var listCommand = new Command("list", "List all transactions");

        listCommand.Options.Add(_categoryOption);


        listCommand.SetAction(parseResult =>
        {
            var category = parseResult.GetValue(_categoryOption);

            Console.WriteLine("📋 Expense list:");
            foreach (var item in _expenseService.GetAllExpenses().Where(expense => expense.Category == category))
            {
                Console.WriteLine($"{item.Id} | {item.Description} | {item.Amount} | {item.Category} | {item.Date}");
            }
            return 0;
        });
        return listCommand;
    }

}
