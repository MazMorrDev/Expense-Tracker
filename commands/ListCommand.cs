using System.CommandLine;

namespace ExpenseTracker;

public static class ListCommand
{
    public static Command GetListCommand()
    {
        var listCommand = new Command("list", "Lista todos los gastos");
        listCommand.SetAction(parseResult =>
        {
            Console.WriteLine("📋 Lista de gastos:");
            // Aquí irá la lógica para leer y mostrar los gastos guardados
            return 0;
        });
        return listCommand;
    }

}
