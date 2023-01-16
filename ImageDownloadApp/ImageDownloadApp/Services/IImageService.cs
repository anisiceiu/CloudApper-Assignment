using System.Collections.Concurrent;

namespace ImageDownloadApp.Services
{
    public interface IImageService
    {
        /// <summary>
        /// Download image and then store in the server
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<IDictionary<string, string>> DownloadImageAsync(List<string> urls, int limit);
        /// <summary>
        /// Get image from server for serving as base64 string
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<string> GetBase64ImageByName(string fileName);
    }
}
