namespace Library;

public static class Constants
{
    public const string BooksFileName = "SavedBooks.json";
    
    public static class ConsoleValues
    {
        public const string AddNewBookCode = "1";
        public const string SearchBookCode = "2";
        public const string DeleteBookCode = "3";
        public const string ShowAllBooksCode = "4";
        public const string ChangeBookStatusCode = "5";
        public const string CloseProgramCode = "0";
    }
    
    public static class ErrorMessages
    {
        public const string IncorrectBookCode = "The book code is incorrect.";
        public const string BookAlreadyExists = "Book with this code already exists.";
        public const string FailedOperation = "The operation failed.";
    }
}