using System.CommandLine;

namespace ExpenseTracker;

public static class AddCommand
{
    public static Command GetAddCommand()
    {
        // Crear el comando 'add' y asociarle las opciones definidas
        //    Un Command representa una acción que el usuario puede ejecutar (ej: "expense-tracker add ...")
        var addCommand = new Command("add", "Add a new Expense");
        addCommand.Options.Add(GeneralFlags.GetDescriptionFlag());
        addCommand.Options.Add(GeneralFlags.GetAmountFlag());
        addCommand.Options.Add(GeneralFlags.GetDescriptionFlag());

        // Definir la lógica que se ejecutará cuando el usuario escriba el comando 'add'
        //    SetAction recibe un delegado que toma un ParseResult (resultado del análisis de la línea de comandos)
        //    y retorna un int (código de salida). El análisis ya validó que los flags requeridos existen.
        addCommand.SetAction(parseResult =>
        {
            // GetValue<T> extrae el valor del flag tipado. Como Required=true aseguramos que no sean null.
            // El operador '!' (null-forgiving) le dice al compilador que confiamos en que no será nulo.
            string description = parseResult.GetValue(GeneralFlags.GetDescriptionFlag())!;
            decimal amount = parseResult.GetValue(GeneralFlags.GetAmountFlag());
            // Para category, DefaultValueFactory garantiza que siempre tenga un valor (nunca null)
            string category = parseResult.GetValue(GeneralFlags.GetDescriptionFlag())!;

            Console.WriteLine($"✅ Added Expense:");
            Console.WriteLine($"   Description: {description}");
            Console.WriteLine($"   Amount: ${amount}");
            Console.WriteLine($"   Category: {category}");

            return 0; // 0 indica éxito
        });
        return addCommand;
    }
}
