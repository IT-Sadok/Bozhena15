using System.Text.Json;
using Library.Entities;
using Library.Helpers;
using Microsoft.Extensions.Logging;

namespace Library.Services;

public class Repository(
    IFileService fileService,
    ILogger<Repository> logger) : IRepository
{
    private readonly ILogger<Repository> _logger = logger;
    private readonly IFileService _fileService = fileService;
    
    public List<T>? GetData<T>() where T : BaseEntity
    {
        try
        {
            var content = _fileService.ReadFileData(typeof(T));

            if (string.IsNullOrEmpty(content))
                return [];
            
            var data = JsonSerializer.Deserialize<List<T>>(content);
            return data ?? [];
        }
        catch (Exception e)
        {
            _logger.LogError("{Service}.{Method}: Failed to retrieve data from the file." + 
                             " Exception: {exception}.",
                nameof(Repository), 
                nameof(GetData), 
                e.Message);
            
            return null;
        }
    }

    public bool AddRecords<T>(List<T> newRecords) where T : BaseEntity
    {
        try
        {
            var recordsFromFile = GetData<T>();
            
            if(recordsFromFile is null)
                return false;
            
            recordsFromFile.AddRange(newRecords);
            
            SaveChanges(recordsFromFile);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Service}.{Method}: Failed to save data to the file." + 
                             " Exception: {exception}.",
                nameof(Repository), 
                nameof(AddRecords), 
                e.Message);            
            
            return false;
        }
    }
    
    public bool DeleteRecords<T>(List<T> deletedRecords) where T : BaseEntity
    {
        try
        {
            var deletedRecordIds = deletedRecords.Select(x => x.Id).ToList();
            var recordsFromFile = GetData<T>();
            
            if(recordsFromFile is null)
                return false;
            
            recordsFromFile = recordsFromFile
                .Where(x => !deletedRecordIds.Contains(x.Id)).ToList();
            
            SaveChanges(recordsFromFile);
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Service}.{Method}: Failed to delete data from the file." + 
                             " Exception: {exception}.",
                nameof(Repository), 
                nameof(DeleteRecords), 
                e.Message); 
            
            return false;
        }
    }

    public bool UpdateRecords<T>(List<T> updatedRecords) where T : BaseEntity
    {
        try
        {
            var updatedRecordsIds = updatedRecords.Select(x => x.Id).ToList();
            var recordsFromFile = GetData<T>();
            
            if(recordsFromFile is null)
                return false;
            
            recordsFromFile = recordsFromFile
                .Where(x => !updatedRecordsIds.Contains(x.Id)).ToList();

            recordsFromFile.AddRange(updatedRecords);
            
            SaveChanges(recordsFromFile);
            
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("{Service}.{Method}: Failed to update data in the file." + 
                             " Exception: {exception}.",
                nameof(Repository), 
                nameof(UpdateRecords), 
                e.Message);

            return false;
        }
    }
    
    private void SaveChanges<T>(List<T> records) where T : BaseEntity
    {
        var content = JsonSerializer.Serialize(records);

        _fileService.WriteFileData(typeof(T), content);
    }
}