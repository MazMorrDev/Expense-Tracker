using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker.commands;

public class AddCommand(ExpenseService expenseService)
{
    private readonly ExpenseService _expenseService = expenseService;

    public Option<string> GetDescriptionFlag()
    {
        // 1. Definir las opciones (flags) que aceptará nuestro comando 'add'
        //    Cada Option<T> representa un flag con su tipo, descripción y reglas.
        var descriptionOption = new Option<string>("--description", "Expense description")
        {
            Required = true       // El usuario debe proporcionar este flag
        };
        return descriptionOption;
    }

    public Option<decimal> GetAmountFlag()
    {
        var amountOption = new Option<decimal>("--amount", "Expense amount")
        {
            Required = true
        };
        return amountOption;
    }

    public Option<string> GetCategoryFlag()
    {
        // Para valor por defecto, usamos DefaultValueFactory (función que se ejecuta si el flag no se proporciona)
        var categoryOption = new Option<string>("--category", "Expense category")
        {
            DefaultValueFactory = _ => "General"  // Si no se usa --category, su valor será "General"
        };
        return categoryOption;
    }

    public Command GetAddCommand()
    {
        // Crear el comando 'add' y asociarle las opciones definidas
        //    Un Command representa una acción que el usuario puede ejecutar (ej: "expense-tracker add ...")
        var addCommand = new Command("add", "Add a new Expense");
        addCommand.Options.Add(GetDescriptionFlag());
        addCommand.Options.Add(GetAmountFlag());
        addCommand.Options.Add(GetDescriptionFlag());

        // Definir la lógica que se ejecutará cuando el usuario escriba el comando 'add'
        //    SetAction recibe un delegado que toma un ParseResult (resultado del análisis de la línea de comandos)
        //    y retorna un int (código de salida). El análisis ya validó que los flags requeridos existen.
        addCommand.SetAction(parseResult =>
        {
            // GetValue<T> extrae el valor del flag tipado. Como Required=true aseguramos que no sean null.
            // El operador '!' (null-forgiving) le dice al compilador que confiamos en que no será nulo.
            string description = parseResult.GetValue(GetDescriptionFlag())!;
            decimal amount = parseResult.GetValue(GetAmountFlag());
            // Para category, DefaultValueFactory garantiza que siempre tenga un valor (nunca null)
            string category = parseResult.GetValue(GetDescriptionFlag())!;

            _expenseService.CreateExpense(description, category, amount);

            Console.WriteLine($"✅ Added Expense:");
            Console.WriteLine($"   Description: {description}");
            Console.WriteLine($"   Amount: ${amount}");
            Console.WriteLine($"   Category: {category}");

            return 0; // 0 indica éxito
        });
        return addCommand;
    }
}
