using System.ComponentModel.DataAnnotations;

namespace services.Models.Attributes
{
    public class FileMimeTypesAttribute : ValidationAttribute
    {
        private readonly HashSet<string> _fileMimeTypes;

        public FileMimeTypesAttribute (params string[] fileMimeTypes)
        {
            _fileMimeTypes = new HashSet<string>(fileMimeTypes, StringComparer.OrdinalIgnoreCase);
            ErrorMessage = $"Разрешённые MIME-типы: {string.Join(", ", fileMimeTypes)}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is not IFormFile file) return new ValidationResult("Некорректный тип файла");

            if (!_fileMimeTypes.Contains(file.ContentType)) return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}