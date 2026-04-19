using Library.Extensions;

namespace Library.Helpers;

public static class FileHelper
{
    public static void CreateDataFilesIfNotExist()
    {
        if(!File.Exists(Constants.BooksFileName))
            using (File.Create(Constants.BooksFileName)) ;
    }

    public static string? ReadFileData(Type type)
        => File.ReadAllText(type.GetFileName());
    
    public static void WriteFileData(Type type, string content)
        => File.WriteAllText(type.GetFileName(), content);
}