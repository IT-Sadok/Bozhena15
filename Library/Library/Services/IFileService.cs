namespace Library.Services;

public interface IFileService
{
    void CreateDataFilesIfNotExist();
    string? ReadFileData(Type type);
    void WriteFileData(Type type, string content);
}