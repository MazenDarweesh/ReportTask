using System;

namespace Domain.Utilities;

public static class UlidTypeConverter
{
    public static Ulid ConvertToUlid(this string source)
    {
        return Ulid.Parse(source);
    }

    public static string ConvertFromUlid(this Ulid source)
    {
        return source.ToString();
    }
}
