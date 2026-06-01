using System.ComponentModel;
using System.Reflection;

namespace SmartHouseManagment.AppCore.Extensions;

public static class EnumExtensions
{
    public static IEnumerable<string> GetAllDescriptions<T>() where T : Enum
        => Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => e.ToEnumDescription());
    
    public static string ToEnumDescription(this Enum value)
        => value.GetType()
               .GetField(value.ToString())
               ?.GetCustomAttribute<DescriptionAttribute>()
               ?.Description
           ?? value.ToString();
}