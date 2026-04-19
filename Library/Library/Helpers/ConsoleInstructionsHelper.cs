namespace Library.Helpers;

public static class ConsoleInstructionsHelper
{
    public static void ShowMenuInstructions()
    {
        Console.WriteLine("Choose an operation:");
        Console.WriteLine(
            """
            1. Add a new book - 1
            2. Search the book - 2
            3. Delete the book - 3
            4. Show all books - 4
            5. Change book's status - 5     
            6. Close the program - 0        
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