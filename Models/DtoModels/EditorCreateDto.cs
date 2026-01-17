using System.ComponentModel.DataAnnotations;
using services.Models.Attributes;

namespace services.Models.DtoModels
{
    public class EditorCreateDto
    {
        [Required(ErrorMessage = "Добавьте изображение услуги")]
        [FileMimeTypes("image/png", "image/jpeg", "image/jpg")]
        [FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "Разрешённые типы файлов: .png, .jpg, .jpeg")]
        [MaxFileSize(1024 * 1024 * 5)]
        public required IFormFile Image { get; set; }

        [MinLength(1, ErrorMessage = "Укажите имя услуги")]
        public required string Name { get; set; }

        [MinLength(1, ErrorMessage = "Укажите описание услуги")]
        public required string Description { get; set; }

        [MinLength(1, ErrorMessage = "Укажите требования услуги")]
        public required string Requirements { get; set; }
    }
}