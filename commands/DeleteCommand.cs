using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class DeleteCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    public Option<int> GetIdFlag()
    {
        // 1. Definir las opciones (flags) que aceptará nuestro comando 'add'
        //    Cada Option<T> representa un flag con su tipo, descripción y reglas.
        var idOption = new Option<int>("--id")
        {
            Required = true,       // El usuario debe proporcionar este flag
            Description = "Expense ID"
        };
        return idOption;
    }

    public Command GetDeleteCommand()
    {
        var deleteCommand = new Command("delete", "Delete an existent Expense");
        deleteCommand.Options.Add(GetIdFlag());

        deleteCommand.SetAction(
            parseResult =>
            {
                int id = parseResult.GetValue(GetIdFlag());
                _expenseService.DeleteExpense(id);
                return 0;
            }
        );
        return deleteCommand;
    }
}
