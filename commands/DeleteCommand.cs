using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class DeleteCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    private readonly Option<int> _idOption = new("--id")
    {
        Required = true,
        Description = "Expense ID"
    };


    public Command GetDeleteCommand()
    {
        var deleteCommand = new Command("delete", "Delete an existent Expense");
        deleteCommand.Options.Add(_idOption);

        deleteCommand.SetAction(
            parseResult =>
            {
                int id = parseResult.GetValue(_idOption);
                _expenseService.DeleteExpense(id);
                return 0;
            }
        );
        return deleteCommand;
    }
}
