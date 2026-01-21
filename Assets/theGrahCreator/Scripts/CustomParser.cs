using System;
using System.Globalization;

/// <summary>
/// Utility class for parsing numeric strings with culture-aware formatting.
/// </summary>
public static class CustomParser
{
    private static readonly CultureInfo[] SupportedCultures =
    {
        CultureInfo.GetCultureInfo("ru-RU"),
        CultureInfo.GetCultureInfo("en-US"),
        CultureInfo.InvariantCulture
    };

    /// <summary>
    /// Parses a string to double, trying multiple culture formats.
    /// </summary>
    /// <param name="value">String value to parse</param>
    /// <returns>Parsed double value or NaN if parsing fails</returns>
    public static double ParseToDouble(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return double.NaN;

        string trimmed = value.Trim();

        foreach (var culture in SupportedCultures)
        {
            if (double.TryParse(trimmed, NumberStyles.Any, culture, out double result))
                return result;
        }

        return double.NaN;
    }
}
