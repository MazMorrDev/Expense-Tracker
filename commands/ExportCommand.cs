using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class ExportCommand(StorageService storageService, ExpenseService expenseService)
{
    private readonly StorageService _storageService = storageService;
    private readonly ExpenseService _expenseService = expenseService;

    private readonly Option<string> _outputOption = new("--output", "-o")
    {
        Required = true,
        Description = "Where are you going to export the file"
    };

    private readonly Option<string> _nameOption = new("--name", "-n")
    {
        DefaultValueFactory = _ => DateTime.Now.ToString(),
        Description = "Defines the name of the file"
    };

    public Command GetExportCommand()
    {
        var exportCommand = new Command("export", "Export the data to a csv file");

        // Agregar las mismas instancias
        exportCommand.Options.Add(_outputOption);
        exportCommand.Options.Add(_nameOption);

        exportCommand.SetAction(parseResult =>
        {
            string output = parseResult.GetValue(_outputOption)!;
            string fileName = parseResult.GetValue(_nameOption)!;

            _storageService.SaveAsCSV(_expenseService.GetAllExpenses(), fileName, output);
            Console.WriteLine($"File exported in: {output}");
        });

        return exportCommand;
    }
}
