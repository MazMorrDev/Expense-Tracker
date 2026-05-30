using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class SummaryCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    private readonly Option<int> _monthOption = new("--month")
    {
        Required = true,       // El usuario debe proporcionar este flag
        Description = "Number of the month of the current year"
    };


    public Command GetSummaryCommand()
    {
        var summaryCommand = new Command("summary", "Shows a summary of all expenses");
        summaryCommand.Options.Add(_monthOption);

        summaryCommand.SetAction(
            parseResult =>
            {
                int month = parseResult.GetValue(_monthOption);
                if (month > 12 || month < 1)
                {
                    Console.WriteLine("Month flag must be between 1 - 12");
                    return 1;
                }
                Console.WriteLine("📋 Expense list:");
                List<Expense> expenses = _expenseService.GetAllExpenses();
                expenses.Where(expense => expense.Date.Month == month);
                foreach (var item in expenses)
                {
                    Console.WriteLine($"{item.Amount} | {item.Category} | {item.Date}");
                }
                return 0;
            }
        );
        return summaryCommand;
    }
}
