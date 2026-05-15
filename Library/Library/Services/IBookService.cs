using Library.Models;
using Library.Entities;

namespace Library.Services;

public interface IBookService
{
    ResultModel<List<Book>> GetAllBooks();
    ResultModel<List<Book>> GetBooksBySearchText(string searchFilter);
    ResultModel<Book?> CreateBook(BookModel bookModel);
    ResultModel<Book?> DeleteBook(string bookCode);
    ResultModel<Book?> UpdateBookStatus(string bookCode);
    ResultModel<Book?> GetBookByCode(string bookCode);
}