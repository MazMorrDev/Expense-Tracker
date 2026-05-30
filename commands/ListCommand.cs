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
            Console.WriteLine("📋 Transaction list:");

            // Aquí irá la lógica para leer y mostrar los gastos guardados
            return 0;
        });
        return listCommand;
    }

}
