using System.CommandLine;

namespace ExpenseTracker;

public static class ListCommand
{
    public static Command GetListCommand()
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
