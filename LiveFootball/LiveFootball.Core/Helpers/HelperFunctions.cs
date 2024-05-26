using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows.Media.Imaging;

using CommunityToolkit.Mvvm.DependencyInjection;

using LiveFootball.Core.ViewModels;

namespace LiveFootball.Core.Helpers;

public static class HelperFunctions
{
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
                    // Console.WriteLine($"Error fetching image from URL {url}: retry {retry}");
            }
        }

        throw new Exception($"Failed to download image after {retryCount} retries");
    }

    private static async Task<BitmapImage> ReadBitmap(string filePath)
    {
        var bitmapImage = new BitmapImage();
        await using var stream = File.OpenRead(filePath);

        bitmapImage.BeginInit();
        bitmapImage.StreamSource = stream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();

        // Console.WriteLine($"Successfully fetched image from file: {filePath}");

        return bitmapImage;
    }

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

    public static void SetLeagueLoadingProgressState(bool state)
    {
        Ioc.Default.GetRequiredService<ResultsViewModel>().IsLoading = state;
        Ioc.Default.GetRequiredService<FixturesViewModel>().IsLoading = state;
        Ioc.Default.GetRequiredService<LeagueStandingViewModel>().IsLoading = state;
    }

    public static void SetLiveGamesLoadingProgressState(bool state)
    {
        Ioc.Default.GetRequiredService<LiveGamesViewModel>().IsLoading = state;
    }

    public static void SetAllGamesLoadingProgressState(bool state)
    {
        Ioc.Default.GetRequiredService<ResultsViewModel>().IsLoading = state;
        Ioc.Default.GetRequiredService<FixturesViewModel>().IsLoading = state;
    }
}