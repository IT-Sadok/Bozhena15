using FluentValidation;
using Library.Models;
using Library.Services;

namespace Library.Validations;

public class CreateBookValidator : AbstractValidator<BookModel>
{
    public CreateBookValidator(
        IBookService bookService)
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(5)
            .Must(x => bookService.GetBookByCode(x) is null)
            .WithMessage(Constants.ErrorMessages.BookAlreadyExists);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Status)
            .NotEmpty();
        
        RuleFor(x => x.Year)
            .InclusiveBetween(1, DateTime.Now.Year);
        
        RuleFor(x => x.AuthorFullName)
            .NotEmpty()
            .MaximumLength(200);
    }
}