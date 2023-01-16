using System.ComponentModel.DataAnnotations;

namespace ImageDownloadApp.DTOs.Requests
{
    public class RequestDownload
    {
        [Required]
        public IEnumerable<string> ImageUrls { get; set; }
        [Required]
        [Range(1, 1000)]
        public int MaxDownloadAtOnce { get; set; }
    }
}
