using System.Text.Json;
using Library.Entities;
using Library.Helpers;

namespace Library.Services;

public class RepositoryService : IRepositoryService
{
    public List<T> GetData<T>() where T : BaseEntity
    {
        try
        {
            var content = FileHelper.ReadFileData(typeof(T));

            if (string.IsNullOrEmpty(content))
                return [];
            
            var data = JsonSerializer.Deserialize<List<T>>(content);
            return data ?? [];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return [];
        }
    }

    public bool AddRecords<T>(List<T> newRecords) where T : BaseEntity
    {
        try
        {
            var recordsFromFile = GetData<T>();
            recordsFromFile.AddRange(newRecords);
            
            SaveChanges(recordsFromFile);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public bool DeleteRecords<T>(List<T> deletedRecords) where T : BaseEntity
    {
        try
        {
            var deletedRecordIds = deletedRecords.Select(x => x.Id).ToList();
            var recordsFromFile = GetData<T>();
            
            recordsFromFile = recordsFromFile
                .Where(x => !deletedRecordIds.Contains(x.Id)).ToList();
            
            SaveChanges(recordsFromFile);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool UpdateRecords<T>(List<T> updatedRecords) where T : BaseEntity
    {
        try
        {
            var updatedRecordsIds = updatedRecords.Select(x => x.Id).ToList();
            var recordsFromFile = GetData<T>();
            
            recordsFromFile = recordsFromFile
                .Where(x => !updatedRecordsIds.Contains(x.Id)).ToList();

            recordsFromFile.AddRange(updatedRecords);
            
            SaveChanges(recordsFromFile);
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
            
        }
    }
    
    private void SaveChanges<T>(List<T> records) where T : BaseEntity
    {
        var content = JsonSerializer.Serialize(records);

        FileHelper.WriteFileData(typeof(T), content);
    }
}