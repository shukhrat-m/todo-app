using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace API.Extensions;

public static class EnumExtension
{
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
        where TAttribute : Attribute
    {
        return enumValue.GetType()
                        .GetMember(enumValue.ToString())
                        .First()
                        .GetCustomAttribute<TAttribute>();
    }

    public static string GetName(this Enum enumValue)
    {
        return enumValue.GetAttribute<DisplayAttribute>().Name;
    }

    public static string GetShortName(this Enum enumValue)
    {
        return enumValue.GetAttribute<DisplayAttribute>().ShortName;
    }
}
