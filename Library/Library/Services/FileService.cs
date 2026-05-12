using Library.Extensions;

namespace Library.Services;

public class FileService : IFileService
{
    public void CreateDataFilesIfNotExist()
    {
        if(File.Exists(Constants.BooksFileName))
            return;
        
        using (File.Create(Constants.BooksFileName)) ;
    }

    public string? ReadFileData(Type type)
        => File.ReadAllText(type.GetFileName());
    
    public void WriteFileData(Type type, string content)
        => File.WriteAllText(type.GetFileName(), content);
}