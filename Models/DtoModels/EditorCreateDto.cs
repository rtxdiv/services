using System.ComponentModel.DataAnnotations;
using services.Models.Attributes;

namespace services.Models.DtoModels
{
    public class EditorCreateDto
    {
        [FileExtension(".png", ".jpg", ".jpeg")]
        [FileMimeTypes("image/png", "image/jpeg", "image/jpg")]
        [MaxFileSize(1024 * 1024 * 5)]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Добавьте имя услуги")]
        [MinLength(1, ErrorMessage = "Укажите имя услуги")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Добавьте описание услуги")]
        [MinLength(1, ErrorMessage = "Укажите описание услуги")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Добавьте требования услуги")]
        [MinLength(1, ErrorMessage = "Укажите требования услуги")]
        public required string Requirements { get; set; }
    }
}