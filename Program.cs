using System.CommandLine;

// 1. Definir las opciones (flags) que aceptará nuestro comando 'add'
//    Cada Option<T> representa un flag con su tipo, descripción y reglas.
var descriptionOption = new Option<string>(
    "--description",      // Nombre largo del flag
    "Descripción del gasto" // Descripción que aparece en la ayuda
)
{
    Required = true       // El usuario debe proporcionar este flag
};

var amountOption = new Option<decimal>(
    "--amount",
    "Monto del gasto"
)
{
    Required = true
};

// Para valor por defecto, usamos DefaultValueFactory (función que se ejecuta si el flag no se proporciona)
var categoryOption = new Option<string>(
    "--category",
    "Categoría del gasto"
)
{
    DefaultValueFactory = _ => "General"  // Si no se usa --category, su valor será "General"
};

// 2. Crear el comando 'add' y asociarle las opciones definidas
//    Un Command representa una acción que el usuario puede ejecutar (ej: "expense-tracker add ...")
var addCommand = new Command("add", "Agrega un nuevo gasto");
addCommand.Options.Add(descriptionOption);
addCommand.Options.Add(amountOption);
addCommand.Options.Add(categoryOption);

// 3. Definir la lógica que se ejecutará cuando el usuario escriba el comando 'add'
//    SetAction recibe un delegado que toma un ParseResult (resultado del análisis de la línea de comandos)
//    y retorna un int (código de salida). El análisis ya validó que los flags requeridos existen.
addCommand.SetAction(parseResult =>
{
    // GetValue<T> extrae el valor del flag tipado. Como Required=true aseguramos que no sean null.
    // El operador '!' (null-forgiving) le dice al compilador que confiamos en que no será nulo.
    string description = parseResult.GetValue(descriptionOption)!;
    decimal amount = parseResult.GetValue(amountOption);
    // Para category, DefaultValueFactory garantiza que siempre tenga un valor (nunca null)
    string category = parseResult.GetValue(categoryOption)!;

    Console.WriteLine($"✅ Gasto agregado:");
    Console.WriteLine($"   Descripción: {description}");
    Console.WriteLine($"   Monto: ${amount}");
    Console.WriteLine($"   Categoría: {category}");

    return 0; // 0 indica éxito
});

// 4. Comando 'list' (sin flags por ahora)
var listCommand = new Command("list", "Lista todos los gastos");
listCommand.SetAction(parseResult =>
{
    Console.WriteLine("📋 Lista de gastos:");
    // Aquí irá la lógica para leer y mostrar los gastos guardados
    return 0;
});

// 5. Comando raíz: es el punto de entrada de toda la aplicación CLI.
//    Agrupa todos los subcomandos y define la descripción global.
var rootCommand = new RootCommand("Expense Tracker - Controla tus gastos");
rootCommand.Subcommands.Add(addCommand);   // Añade "add" al root
rootCommand.Subcommands.Add(listCommand);  // Añade "list" al root

// 6. Parsear los argumentos reales (los que vienen de la terminal) y ejecutar el comando correspondiente.
//    - Parse: Analiza la cadena de argumentos según las reglas definidas.
//    - Invoke: Ejecuta la acción del comando que coincidió y devuelve el código de salida.
ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();