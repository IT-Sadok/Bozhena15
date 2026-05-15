using Library.Models;
using Library.Entities;

namespace Library.Mappers;

public static class MapToModel
{
    public static Book GetBook(BookModel book)
        => new()
        {
            Code = book.Code,
            Name = book.Name,
            AuthorFullName = book.AuthorFullName,
            Year = book.Year,
            Status = book.Status,
        };
}