namespace ExpenseTracker;

public static class CommandsHandler
{
    public static void ChooseCommands(string[] args)
    {
        switch (args[0])
        {
            case "add":
                decimal amount = decimal.Parse(args[1]);
                // Lógica para agregar gasto
                break;
            case "list":
                // Mostrar lista
                break;
            case "summary":
                break;
            case "delete":
                break;
            case "help":
                Console.WriteLine("Comandos disponibles: add, list, help");
                break;
        }
    }
}
