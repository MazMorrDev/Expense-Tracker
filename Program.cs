using System.CommandLine;
using ExpenseTracker;

// Comando raíz: es el punto de entrada de toda la aplicación CLI.
//    Agrupa todos los subcomandos y define la descripción global.
var rootCommand = new RootCommand("Expense Tracker - Your expenses on your hand");
rootCommand.Subcommands.Add(AddCommand.GetAddCommand());   // Añade "add" al root
rootCommand.Subcommands.Add(ListCommand.GetListCommand());  // Añade "list" al root

// Parsear los argumentos reales (los que vienen de la terminal) y ejecutar el comando correspondiente.
//    - Parse: Analiza la cadena de argumentos según las reglas definidas.
//    - Invoke: Ejecuta la acción del comando que coincidió y devuelve el código de salida.
ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();