namespace Library.Helpers;

public static class ConsoleInstructionsHelper
{
    public static void ShowMenuInstructions()
    {
        Console.WriteLine("Choose an operation:");
        Console.WriteLine(
            $"""
            1. Add a new book - {Constants.ConsoleValues.AddNewBookCode}
            2. Search the book - {Constants.ConsoleValues.SearchBookCode}
            3. Delete the book - {Constants.ConsoleValues.DeleteBookCode}
            4. Show all books - {Constants.ConsoleValues.ShowAllBooksCode}
            5. Change book's status - {Constants.ConsoleValues.ChangeBookStatusCode}
            6. Close the program - {Constants.ConsoleValues.CloseProgramCode}        
            """);
    }

    public static string? WriteFieldInstructions(string filedName)
    {
        Console.WriteLine($"{filedName}:");
        var value = Console.ReadLine();
        return value;
    }

    public static void ShowValidatorErrorMessages(IEnumerable<string> errorMessages)
    {
        foreach (var errorMessage in errorMessages)
            Console.WriteLine(errorMessage);
    }

    public static void ShowRecords<T>(IEnumerable<T> records)
    {
        foreach (var record in records)
        {
            var fields = record?.GetType().GetProperties();

            if (fields == null || fields.Length == 0)
                continue;

            foreach (var field in fields)
            {
                Console.WriteLine($"{field.Name}: {field.GetValue(record)}");
            }
            
            Console.WriteLine("------------------------------------------------");
        }
    }
}