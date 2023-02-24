using API.DTOs;
using API.Extensions;

namespace API.Common;

public static class EnumHelper
{
    public static StatusesEnum? CastToStatusesEnum(string status)
    {
        var values = Enum.GetValues(typeof(StatusesEnum)).Cast<StatusesEnum>();
        foreach (var item in values)
        {
            if (item.GetName().Equals(status, StringComparison.InvariantCultureIgnoreCase))
            {
                return item;
            }
        }

        return null;
    }
}
