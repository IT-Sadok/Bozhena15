using Library.Models;
using Library.Entities;
using Library.Helpers;

namespace Library.Services;

public class InputConsoleService : IInputConsoleService
{
    public BookModel InputBookData()
        => new()
        {
            Code = InputFieldText("Book Code"),
            Name = InputFieldText("Book Name"),
            AuthorFullName = InputFieldText("Author Full Name"),
            Year = InputFieldInt("Year"),
            Status = BookStatus.Free
        };

    public string InputFieldText(string fieldTextName)
        => ConsoleInstructionsHelper.WriteFieldInstructions(fieldTextName) ?? string.Empty;
    
    private int InputFieldInt(string fieldTextName)
    {
        var value = InputFieldText(fieldTextName);
        return int.TryParse(value, out var result) ? result : 0;
    }
}