using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace LiveFootballUnitTests;

/// <summary>
/// Helper class for deserializer in unit tests.
/// </summary>
internal static class DeserializerUnitTestHelper
{
    /// <summary>
    /// Reads a file from the Assets folder asynchronously.
    /// </summary>
    internal static async Task<string> ReadFileFromAssetsAsync(string fileName)
    {
        // Build the file path
        string filePath = Path.Combine(AppContext.BaseDirectory, "Assets", fileName);

        // Read the file asynchronously and return the content
        using StreamReader reader = new StreamReader(filePath);
        return await reader.ReadToEndAsync();
    }

    internal static string ConvertDateTimeToString(int year, int month, int day, int hour, int minute, int second) =>
        new DateTime(year, month, day, hour, minute, second).ToLocalTime().ToString("MMM d, HH:mm", CultureInfo.CurrentCulture);
}