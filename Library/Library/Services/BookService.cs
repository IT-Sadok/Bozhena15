using Library.Dtos;
using Library.Entities;
using Library.Helpers;
using Library.Mappers;
using Library.Validations;

namespace Library.Services;

public class BookService(
     IRepositoryService repositoryService) : IBookService
{
     private readonly IRepositoryService _repositoryService = repositoryService;

     public List<Book> GetAllBooks()
          => _repositoryService.GetData<Book>();
     
     public List<Book> GetBooksBySearchText(string searchFilter)
     {
          var books = _repositoryService.GetData<Book>();

          var filteredBooks = books.Where(x => 
               x.AuthorFullName.Contains(searchFilter, StringComparison.OrdinalIgnoreCase) 
               || x.Name.Contains(searchFilter, StringComparison.OrdinalIgnoreCase))
               .ToList();
          
          return filteredBooks;
     }
     
     public ResultDto CreateBook(BookDto bookDto)
     {
          var validator = new CreateBookValidator(this);
          var result = validator.Validate(bookDto);
          
          if(!result.IsValid)
          {
               var errors = result.Errors.Select(x => x.ErrorMessage);
               return new ResultDto(IsError: result.IsValid, errors);
          }

          var book = BookMapper.GetBook(bookDto);
          
          var isSuccessResult = _repositoryService.AddRecords([book]);

          return !isSuccessResult
               ? new ResultDto(IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultDto();
     }
     
     public ResultDto DeleteBook(string bookCode)
     {
          var book = GetBookByCode(bookCode);
          
          if(book is null)
               return new ResultDto(IsError: true, [Constants.ErrorMessages.IncorrectBookCode]);
          
          var isSuccessResult = _repositoryService.DeleteRecords([book]);
          
          return !isSuccessResult
               ? new ResultDto(IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultDto();
     }
     
     public ResultDto UpdateBookStatus(string bookCode)
     {
          var book = GetBookByCode(bookCode);
          
          if(book is null)
               return new ResultDto(IsError: true, [Constants.ErrorMessages.IncorrectBookCode]);
          
          book.Status = GetNewBookStatus(book.Status);
          
          var isSuccessResult = _repositoryService.UpdateRecords([book]);
          
          return !isSuccessResult
               ? new ResultDto(IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultDto();
     }
     
     public Book? GetBookByCode(string bookCode)
     {
          if(string.IsNullOrWhiteSpace(bookCode))
               return null;
          
          var books = _repositoryService.GetData<Book>();
          
          return books.FirstOrDefault(x => x.Code == bookCode);
     }
     
     private static BookStatus GetNewBookStatus(BookStatus currentStatus)
          => currentStatus == BookStatus.Booked ?  BookStatus.Free : BookStatus.Booked;
}