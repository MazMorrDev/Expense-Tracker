using System.CommandLine;
using ExpenseTracker.commands;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class DeleteCommand(GeneralFlags generalFlags, ExpenseService expenseService)
{
    private readonly GeneralFlags _generalFlags = generalFlags;
    private readonly ExpenseService _expenseService = expenseService;

    public Command GetDeleteCommand()
    {
        var deleteCommand = new Command("delete", "Delete an existent Expense");
        deleteCommand.Options.Add(_generalFlags.GetIdFlag());

        deleteCommand.SetAction(
            parseResult =>
            {
                int id = parseResult.GetValue(_generalFlags.GetIdFlag());
                _expenseService.DeleteExpense(id);
                return 0;
            }
        );
        return deleteCommand;
    }
}
