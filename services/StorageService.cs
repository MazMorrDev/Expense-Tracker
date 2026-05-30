using System.Text.Json;

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

    public void SaveAsCSV(List<Expense>? expenses, )
    {
        string csv = Csv
    }
}