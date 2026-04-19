using Library.Dtos;
using Library.Entities;

namespace Library.Services;

public interface IBookService
{
    List<Book> GetAllBooks();
    List<Book> GetBooksBySearchText(string searchFilter);
    ResultDto CreateBook(BookDto bookDto);
    ResultDto DeleteBook(string bookCode);
    ResultDto UpdateBookStatus(string bookCode);
    Book? GetBookByCode(string bookCode);
}