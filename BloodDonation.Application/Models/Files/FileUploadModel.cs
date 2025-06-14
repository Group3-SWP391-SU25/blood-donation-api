using Microsoft.AspNetCore.Http;

namespace BloodDonation.Application.Models.Files
{
    public class FileUploadModel
    {
        public IFormFile File { get; set; }
        public string? Directory { get; set; } = string.Empty;
    }
    public class FileResponseModel
    {
        public string FileUrl { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }

}
