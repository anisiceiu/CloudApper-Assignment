using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ImageDownloadApp.Services
{
    public class ImageService : IImageService
    {
        IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment hostEnvironment)
        {
            _webHostEnvironment = hostEnvironment;
        }
        public async Task<IDictionary<string, string>> DownloadImageAsync(List<string> urls, int limit)
        {
            SemaphoreSlim semaphoreSlim = new SemaphoreSlim(limit);
            List<Task> downloadImagesTasks = new List<Task>();
            ConcurrentQueue<string> urlsQueue = new ConcurrentQueue<string>();
            ConcurrentDictionary<string, string> ulrNames = new ConcurrentDictionary<string, string>();


            for (int i = 0; i < urls.Count; i++)
            {
                urlsQueue.Enqueue(urls[i]);
            }

            while (downloadImagesTasks.Count <= urls.Count)
            {
                await semaphoreSlim.WaitAsync();

                downloadImagesTasks.Add(Task.Run(async () =>
                {
                    if (urlsQueue.TryDequeue(out var url))
                    {
                        var imageName = await GetAndSaveImageAsync(url);

                        ulrNames.TryAdd(url, imageName);

                        semaphoreSlim.Release(1);
                    }

                }));
            }

            await Task.WhenAll(downloadImagesTasks);

            return ulrNames;
        }

        public async Task<string> GetBase64ImageByName(string fileName)
        {
            string imagePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "Resources","Images", fileName);

            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"{fileName} Not Found");
            }

            byte[] imageArray = await System.IO.File.ReadAllBytesAsync(imagePath);

            string base64Image = Convert.ToBase64String(imageArray);

            return base64Image;
        }

        #region Private Methods
        async Task<string> GetAndSaveImageAsync(string url)
        {
            string imageName = $"{Guid.NewGuid()}.png";

            string filePath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "Resources","Images");

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.SendAsync(httpRequest).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();

                    filePath = System.IO.Path.Combine(filePath, imageName);
                    File.WriteAllBytes(filePath, imageBytes);

                }
            }


            return imageName;
        }
        #endregion
    }
}
