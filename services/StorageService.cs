using System.Globalization;
using System.Text;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;

namespace ExpenseTracker.services;

public class StorageService
{
    private readonly string _dataFilePath;

    public StorageService()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        var dataDir = Path.Combine(projectRoot, "data");

        if (!Directory.Exists(dataDir))
            Directory.CreateDirectory(dataDir);

        _dataFilePath = Path.Combine(dataDir, "data.json");
    }

    public List<Expense> Load()
    {
        if (!File.Exists(_dataFilePath))
            return [];

        string json = File.ReadAllText(_dataFilePath);


        if (string.IsNullOrWhiteSpace(json))
            return [];

        try
        {
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? [];
        }
        catch (JsonException)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The file data.json it's damaged or doesn't have a valid format.");
            Console.ResetColor();
            return [];
        }
    }

    public void Save(List<Expense>? expenses)
    {
        string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_dataFilePath, json);
    }

    public void SaveAsCSV(List<Expense> expenses, string fileName, string outputPath)
    {
        string fullPath;

        if (Directory.Exists(outputPath) || outputPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            fullPath = Path.Combine(outputPath, $"{fileName}.csv");
        }
        else
        {
            // if the user puts a route like "/home/user/my-expenses.csv"
            fullPath = outputPath;
        }

        string? directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Encoding = Encoding.UTF8
        };

        using var writer = new StreamWriter(fullPath);
        using var csv = new CsvWriter(writer, config);
        csv.WriteRecords(expenses);

        Console.WriteLine($"✅ CSV saved to: {fullPath}");
    }
}