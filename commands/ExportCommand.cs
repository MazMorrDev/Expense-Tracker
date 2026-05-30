using System.CommandLine;
using ExpenseTracker.services;

namespace ExpenseTracker;

public class ExportCommand(StorageService storageService)
{
    private readonly StorageService _storageService = storageService;

    private readonly Option<string> _outputOption = new("--output", "-o")
    {
        Required = true,
    };

    private readonly Option<string> _nameOption = new("--name", "-n")
    {
        DefaultValueFactory = _ => DateTime.Now.ToString()
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
            _storageService.SaveAsCSV();
            Console.WriteLine($"File exported in: {output}");
        });

        return exportCommand;
    }
}
