using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class SummaryCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    public Command GetSummaryCommand()
    {
        var summaryCommand = new Command("delete", "Delete an existent Expense");
        summaryCommand.Options.Add(_generalFlags.GetIdFlag());

        summaryCommand.SetAction(
            parseResult =>
            {
                int id = parseResult.GetValue(_generalFlags.GetIdFlag());
                _expenseService.DeleteExpense(id);
                return 0;
            }
        );
        return summaryCommand;
    }
}
