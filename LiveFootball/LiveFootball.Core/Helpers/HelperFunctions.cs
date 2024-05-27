/**************************************************************************
 *                                                                        * 
 *  File:        HelperFunctions.cs                                       *
 *  Description: LiveFootball Core Library                                *
 *               Helper class providing various utility methods           *
 *               for handling images, loading progress states, etc.       *
 *               Implements static methods for these utilities.           *
 *  Copyright:   (c) 2024, LiveFootball Team                              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Media.Imaging;

using CommunityToolkit.Mvvm.DependencyInjection;

using LiveFootball.Core.ViewModels;

namespace LiveFootball.Core.Helpers;

/// <summary>
/// Helper class providing various utility methods for handling images, loading progress states, etc.
/// </summary>
public static class HelperFunctions
{
    // <summary>
    /// Gets the team logo image from the specified URL, with caching support.
    /// </summary>
    /// <param name="url">The URL of the team logo image.</param>
    /// <returns>A <see cref="BitmapImage"/> representing the team logo.</returns>
    public static async Task<BitmapImage> GetTeamLogoFromUrl(string url)
    {
        var fileName = Path.GetFileName(url);
        var filePath = $"teams/{fileName}";

        if (File.Exists(filePath))
            return await ReadBitmap(filePath);

        var bitmapImage = await GetImageFromInternet(url);
        if (bitmapImage != null)
            await WriteBitmapAsync(bitmapImage, filePath);

        return bitmapImage;
    }
    
    // <summary>
    /// Gets the league logo image from the specified URL, with caching support.
    /// </summary>
    /// <param name="url">The URL of the league logo image.</param>
    /// <returns>A <see cref="BitmapImage"/> representing the league logo.</returns>
    public static async Task<BitmapImage> GetLeagueLogoFromUrl(string url)
    {
        var fileName = Path.GetFileName(url);
        var filePath = $"leagues/{fileName}";

        if (File.Exists(filePath))
            return await ReadBitmap(filePath);

        var bitmapImage = await GetImageFromInternet(url);
        if (bitmapImage != null)
            await WriteBitmapAsync(bitmapImage, filePath);

        return bitmapImage;
    }

    /// <summary>
    /// Downloads an image from the internet using the specified URL.
    /// </summary>
    /// <param name="url">The URL of the image to download.</param>
    /// <returns>A <see cref="BitmapImage"/> representing the downloaded image.</returns>
    private static async Task<BitmapImage?> GetImageFromInternet(string url)
    {
        using var httpClient = new HttpClient();
        try
        {
            // Download the image data
            var imageData = await DownloadImageWithRetry(url);

            // Create a BitmapImage from the downloaded bytes
            var bitmapImage = new BitmapImage();
            using var stream = new MemoryStream(imageData);
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            Console.WriteLine($"Successfully fetched image from URL {url}");

            return bitmapImage;
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occurred during the download or image creation
            Console.WriteLine($"Error fetching image from URL {url}: {ex.Message}");

            return null;
        }
    }

    /// <summary>
    /// Downloads an image from the internet with retry logic.
    /// </summary>
    /// <param name="url">The URL of the image to download.</param>
    /// <param name="retryCount">The number of retries in case of failure.</param>
    /// <returns>The byte array representing the downloaded image.</returns>
    private static async Task<byte[]> DownloadImageWithRetry(string url, int retryCount = 10)
    {
        using var httpClient = new HttpClient();

        for (var retry = 1; retry <= retryCount; retry++)
        {
            try
            {
                var imageData = await httpClient.GetByteArrayAsync(url);

                return imageData; // Return the image data if download is successful
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.TooManyRequests)
                    await Task.Delay(300); // Wait 300ms before retrying
            }
        }

        throw new Exception($"Failed to download image after {retryCount} retries");
    }

    /// <summary>
    /// Reads a bitmap image from the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the image file to read.</param>
    /// <returns>A <see cref="BitmapImage"/> representing the image read from file.</returns>
    private static async Task<BitmapImage> ReadBitmap(string filePath)
    {
        var bitmapImage = new BitmapImage();
        await using var stream = File.OpenRead(filePath);

        bitmapImage.BeginInit();
        bitmapImage.StreamSource = stream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();

        return bitmapImage;
    }

    /// <summary>
    /// Writes the specified bitmap image to the file system.
    /// </summary>
    /// <param name="bitmapImage">The bitmap image to write.</param>
    /// <param name="filePath">The file path where the image will be written.</param>
    private static async Task WriteBitmapAsync(BitmapImage bitmapImage, string filePath)
    {
        // Ensure the parent directory exists, create it if it doesn't
        var directoryPath = Path.GetDirectoryName(filePath);
        Directory.CreateDirectory(directoryPath);

        // Save the BitmapImage as a PNG file
        try
        {
            // Save the encoded PNG data to the stream on a separate thread
            await Task.Run(() =>
                           {
                               using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write,
                                   FileShare.None, 4096, true);
                               var encoder = new PngBitmapEncoder();
                               encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                               encoder.Save(stream);
                           });
        }
        catch (Exception)
        {
            // ignored
        }
    }

    /// <summary>
    /// Sets the loading progress state for league-related views.
    /// </summary>
    /// <param name="state">The loading state to set.</param>
    public static void SetLeagueLoadingProgressState(bool state)
    {
        Ioc.Default.GetRequiredService<ResultsViewModel>().IsLoading = state;
        Ioc.Default.GetRequiredService<FixturesViewModel>().IsLoading = state;
        Ioc.Default.GetRequiredService<LeagueStandingViewModel>().IsLoading = state;
    }

    /// <summary>
    /// Sets the loading progress state for live games view.
    /// </summary>
    /// <param name="state">The loading state to set.</param>
    public static void SetLiveGamesLoadingProgressState(bool state)
    {
        Ioc.Default.GetRequiredService<LiveGamesViewModel>().IsLoading = state;
    }

    /// <summary>
    /// Sets the loading progress state for all games-related views.
    /// </summary>
    /// <param name="state">The loading state to set.</param>
    public static void SetAllGamesLoadingProgressState(bool state)
    {
        Ioc.Default.GetRequiredService<ResultsViewModel>().IsLoading = state;
        Ioc.Default.GetRequiredService<FixturesViewModel>().IsLoading = state;
    }
}