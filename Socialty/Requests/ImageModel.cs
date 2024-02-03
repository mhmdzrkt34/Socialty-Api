using System.ComponentModel.DataAnnotations;

namespace Socialty.Requests
{
    public class ImageModel
    {
        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".gif" }, ErrorMessage = "Invalid file format. Only JPG, JPEG, PNG, and GIF are allowed.")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 5 MB.")]
        public IFormFile Image { get; set; }
    }
}
