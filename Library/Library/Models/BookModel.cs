using Library.Entities;

namespace Library.Models;

public class BookModel
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string AuthorFullName { get; set; }
    public required int Year { get; set; }
    public required BookStatus Status { get; set; }
}