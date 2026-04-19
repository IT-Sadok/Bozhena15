using Library.Dtos;
using Library.Entities;
using Library.Helpers;

namespace Library.Services;

public class ConsoleMenuService(
    IBookService bookService,
    IInputConsoleService inputConsoleService) : IConsoleMenuService
{
    private readonly IBookService _bookService = bookService;
    private readonly IInputConsoleService _inputConsoleService = inputConsoleService;
    
    public void ShowConsoleMenu()
    {
        var operation = string.Empty;

        while (operation != Constants.ConsoleValues.CloseProgramCode)
        {
            switch (operation)
            {
                case Constants.ConsoleValues.AddNewBookCode:
                    CreateBook();
                    break;  
                case Constants.ConsoleValues.SearchBookCode:
                    SearchBooks();
                    break;
                case Constants.ConsoleValues.DeleteBookCode:
                    DeleteBook();
                    break;
                case Constants.ConsoleValues.ShowAllBooksCode: 
                    ShowAllBooks();
                    break;
                case Constants.ConsoleValues.ChangeBookStatusCode:
                    UpdateBookStatus();
                    break;
            }
            
            ConsoleInstructionsHelper.ShowMenuInstructions();
            operation = Console.ReadLine();
        }
    }

    private void ShowAllBooks()
    {
        var books = _bookService.GetAllBooks();
        ConsoleInstructionsHelper.ShowRecords(books);
    }
    
    private void SearchBooks()
    {
        var searchFilter = _inputConsoleService.InputFieldText("Search Filter");
        var filteredBooks = _bookService.GetBooksBySearchText(searchFilter);
        
        ConsoleInstructionsHelper.ShowRecords(filteredBooks);
    }
    
    private void CreateBook()
    {
        var bookDto = _inputConsoleService.InputBookData();
        var result = _bookService.CreateBook(bookDto);

        if (!result.IsError)
            return;

        ConsoleInstructionsHelper.ShowValidatorErrorMessages(result.Errors ?? []);
    }
    
    private void DeleteBook()
    {
        var bookCode = _inputConsoleService.InputFieldText("Book Code");
        var result = _bookService.DeleteBook(bookCode);
        
        if (!result.IsError)
            return;

        ConsoleInstructionsHelper.ShowValidatorErrorMessages(result.Errors ?? []);
    }
    
    private void UpdateBookStatus()
    {
        var bookCode = _inputConsoleService.InputFieldText("Book Code");
        var result = _bookService.UpdateBookStatus(bookCode);
        
        if (!result.IsError)
            return;

        ConsoleInstructionsHelper.ShowValidatorErrorMessages(result.Errors ?? []);
    }
}