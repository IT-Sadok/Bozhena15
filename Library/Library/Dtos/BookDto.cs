using Library.Entities;

namespace Library.Dtos;

public class BookDto
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string AuthorFullName { get; set; }
    public required int Year { get; set; }
    public required BookStatus Status { get; set; }
}