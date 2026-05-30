using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker.commands;

public class ListCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;
    public Command GetListCommand()
    {
        var listCommand = new Command("list", "List all transactions");
        listCommand.SetAction(parseResult =>
        {
            Console.WriteLine("📋 Expense list:");
            foreach (var item in _expenseService.GetAllExpenses())
            {
                Console.WriteLine($"{item.Id} | {item.Description} | {item.Amount} | {item.Category} | {item.Date}");
            }
            return 0;
        });
        return listCommand;
    }

}
