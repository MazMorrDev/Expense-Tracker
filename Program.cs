using System.CommandLine;
using ExpenseTracker;
using TaskTrackerCLI.Services;



// Root Commands: it's the entry point of all the CLI application.
//    Group all the subcommands and defines the global description.
var storageService = new StorageService();
var expenseService = new ExpenseService(storageService);

var generalFlags = new GeneralFlags();
var addCommand = new AddCommand(generalFlags, expenseService);
var listCommand = new ListCommand(expenseService);
var rootCommand = new RootCommand("Expense Tracker - Oh My Expenses (;D Reference)");
rootCommand.Subcommands.Add(addCommand.GetAddCommand());   // Add the "add" command to the root
rootCommand.Subcommands.Add(listCommand.GetListCommand());

// Parsear los argumentos reales (los que vienen de la terminal) y ejecutar el comando correspondiente.
//    - Parse: Analiza la cadena de argumentos según las reglas definidas.
//    - Invoke: Ejecuta la acción del comando que coincidió y devuelve el código de salida.
ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();