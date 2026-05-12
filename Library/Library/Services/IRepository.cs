using Library.Entities;

namespace Library.Services;

public interface IRepository
{
    List<T>? GetData<T>() where T : BaseEntity;
    bool AddRecords<T>(List<T> newRecords) where T : BaseEntity;
    bool DeleteRecords<T>(List<T> deletedRecords) where T : BaseEntity;
    bool UpdateRecords<T>(List<T> updatedRecords) where T : BaseEntity;
}