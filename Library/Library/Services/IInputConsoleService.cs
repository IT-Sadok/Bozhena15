using Library.Dtos;

namespace Library.Services;

public interface IInputConsoleService
{
    BookDto InputBookData();
    string InputFieldText(string fieldTextName);
}