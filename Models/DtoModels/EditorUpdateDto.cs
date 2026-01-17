using System.ComponentModel.DataAnnotations;
using services.Models.Attributes;

namespace services.Models.DtoModels
{
    public class EditorUpdateDto
    {
        [MaxFileSize(1024 * 1024 * 5)]
        [FileMimeTypes("image/png", "image/jpeg", "image/jpg")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Разрешённые типы файлов: .png, .jpg, .jpeg")]
        public IFormFile? Image { get; set; } = null;

        [MinLength(1, ErrorMessage = "Укажите имя услуги")]
        public required string Name { get; set; }

        [MinLength(1, ErrorMessage = "Укажите описание услуги")]
        public required string Description { get; set; }

        [MinLength(1, ErrorMessage = "Укажите требования услуги")]
        public required string Requirements { get; set; }
    }
}