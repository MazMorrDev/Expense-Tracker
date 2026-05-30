using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class SummaryCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    private readonly Option<int> _monthOption = new("--month")
    {
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
                if (month != 0)
                {
                    if (month > 12 || month < 1)
                    {
                        Console.WriteLine("Month flag must be between 1 - 12");
                        return 1;
                    }
                }

                Console.WriteLine("📋 Expense summary:");
                List<Expense> expenses = _expenseService.GetAllExpenses();
                decimal totalExpense = 0;

                foreach (var item in expenses.Where(expense => expense.Date.Month == month))
                {
                    totalExpense += item.Amount;
                }
                Console.WriteLine($"Total Expense: ${totalExpense}");
                return 0;
            }
        );
        return summaryCommand;
    }
}
