using Library.Models;

namespace Library.Services;

public interface IInputConsoleService
{
    BookModel InputBookData();
    string InputFieldText(string fieldTextName);
}