using Library.Dtos;
using Library.Entities;

namespace Library.Mappers;

public static class BookMapper
{
    public static Book GetBook(BookDto book)
        => new()
        {
            Code = book.Code,
            Name = book.Name,
            AuthorFullName = book.AuthorFullName,
            Year = book.Year,
            Status = book.Status,
        };
}