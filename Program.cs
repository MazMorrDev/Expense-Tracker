using System.CommandLine;
using ExpenseTracker;
using ExpenseTracker.commands;
using ExpenseTracker.services;

// Root Commands: it's the entry point of all the CLI application.
//    Group all the subcommands and defines the global description.
var storageService = new StorageService();
var expenseService = new ExpenseService(storageService);

var addCommand = new AddCommand(expenseService);
var listCommand = new ListCommand(expenseService);
var deleteCommand = new DeleteCommand(expenseService);
var summaryCommand = new SummaryCommand(expenseService);
var rootCommand = new RootCommand("Expense Tracker - Oh My Expenses (;D Reference)");
rootCommand.Subcommands.Add(addCommand.GetAddCommand());   // Add the "add" command to the root
rootCommand.Subcommands.Add(listCommand.GetListCommand());
rootCommand.Subcommands.Add(deleteCommand.GetDeleteCommand());
rootCommand.Subcommands.Add(summaryCommand.GetSummaryCommand());

// Parse the real arguments (the ones that comes from the terminal) and execute the respective command.
//    - Parse: Analize the string of arguments by the defined rules.
//    - Invoke: Execute the action of the command that matched and returns the exit code.
ParseResult parseResult = rootCommand.Parse(args);
return parseResult.Invoke();