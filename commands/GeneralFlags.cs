using System.CommandLine;

namespace ExpenseTracker;

public static class GeneralFlags
{
    public static Option<string> GetDescriptionFlag()
    {
        // 1. Definir las opciones (flags) que aceptará nuestro comando 'add'
        //    Cada Option<T> representa un flag con su tipo, descripción y reglas.
        var descriptionOption = new Option<string>("--description", "Expense description")
        {
            Required = true       // El usuario debe proporcionar este flag
        };
        return descriptionOption;
    }

    public static Option<decimal> GetAmountFlag()
    {
        var amountOption = new Option<decimal>("--amount", "Expense amount")
        {
            Required = true
        };
        return amountOption;
    }

    public static Option<string> GetCategoryFlag()
    {
        // Para valor por defecto, usamos DefaultValueFactory (función que se ejecuta si el flag no se proporciona)
        var categoryOption = new Option<string>("--category", "Expense category")
        {
            DefaultValueFactory = _ => "General"  // Si no se usa --category, su valor será "General"
        };
        return categoryOption;
    }
}
