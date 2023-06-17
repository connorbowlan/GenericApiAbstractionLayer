namespace GenericApiAbstractionLayer.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Pluralizes a string.
    /// </summary>
    /// <param name="value">The string value to pluralize.</param>
    /// <returns>The pluralized string.</returns>
    public static string Pluralize(this string value)
    {
        if (value.Contains(" "))
            return value;

        if (value.EndsWith("y"))
            return value[..^1] + "ies";

        if (value.EndsWith("s"))
            return value + "es";

        return value + "s";
    }
}