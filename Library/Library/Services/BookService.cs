using Library.Models;
using Library.Entities;
using Library.Helpers;
using Library.Mappers;
using Library.Validations;

namespace Library.Services;

public class BookService(
     IRepository repository) : IBookService
{
     private readonly IRepository _repository = repository;

     public ResultModel<List<Book>> GetAllBooks()
     {
          var books = _repository.GetData<Book>();
          
          return books is null
               ? new ResultModel<List<Book>>(Data: [], IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultModel<List<Book>>(Data: books);
     }
     
     public ResultModel<List<Book>> GetBooksBySearchText(string searchFilter)
     {
          var books = _repository.GetData<Book>();

          if (books is null)
               return new ResultModel<List<Book>>(Data: [], IsError: true, [Constants.ErrorMessages.FailedOperation]);
          
          var filteredBooks = books.Where(x => 
               x.AuthorFullName.Contains(searchFilter, StringComparison.OrdinalIgnoreCase) 
               || x.Name.Contains(searchFilter, StringComparison.OrdinalIgnoreCase))
               .ToList();
          
          return new ResultModel<List<Book>>(Data: filteredBooks);
     }
     
     public ResultModel<Book?> CreateBook(BookModel bookModel)
     {
          var validator = new CreateBookValidator(this);
          var result = validator.Validate(bookModel);
          
          if(!result.IsValid)
          {
               var errors = result.Errors.Select(x => x.ErrorMessage);
               return new ResultModel<Book?>(Data: null, IsError: result.IsValid, errors);
          }

          var book = MapToModel.GetBook(bookModel);
          
          var isSuccessResult = _repository.AddRecords([book]);

          return !isSuccessResult
               ? new ResultModel<Book?>(Data: null, IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultModel<Book?>(Data: book);
     }
     
     public ResultModel<Book?> DeleteBook(string bookCode)
     {
          var result = GetBookByCode(bookCode);
          
          if(result.IsError || result.Data is null)
               return result;
          
          var isSuccessResult = _repository.DeleteRecords([result.Data]);
          
          return !isSuccessResult
               ? new ResultModel<Book?>(Data: null, IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultModel<Book?>(Data: null);
     }
     
     public ResultModel<Book?> UpdateBookStatus(string bookCode)
     {
          var result = GetBookByCode(bookCode);

          if (result.IsError || result.Data is null)
               return result;
          
          var book = result.Data!;
          book.Status = GetNewBookStatus(book.Status);
          
          var isSuccessResult = _repository.UpdateRecords([book]);
          
          return !isSuccessResult
               ? new ResultModel<Book?>(Data: null, IsError: true, [Constants.ErrorMessages.FailedOperation])
               : new ResultModel<Book?>(Data: null);
     }
     
     public ResultModel<Book?> GetBookByCode(string bookCode)
     {
          if(string.IsNullOrWhiteSpace(bookCode))
               return new ResultModel<Book?>(Data: null, IsError: true, [Constants.ErrorMessages.IncorrectBookCode]);
          
          var books = _repository.GetData<Book>();
          var book = books?.FirstOrDefault(x => x.Code == bookCode);
          
          return new ResultModel<Book?>(Data: book);
     }
     
     private static BookStatus GetNewBookStatus(BookStatus currentStatus)
          => currentStatus == BookStatus.Booked ?  BookStatus.Free : BookStatus.Booked;
}