using Library.Entities;

namespace Library.Extensions;

public static class TypeExtension
{
    public static string GetFileName(this Type type)
    {
        var typeName = type.Name;
        
        if (typeName == nameof(Book))
            return Constants.BooksFileName;
        
        throw new ArgumentException($"Type {type} is not supported.");
    }
}