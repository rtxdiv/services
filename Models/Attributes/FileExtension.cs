using System.ComponentModel.DataAnnotations;

namespace services.Models.Attributes
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        private readonly HashSet<string> _fileExtensions;

        public FileExtensionAttribute (params string[] fileExtensions)
        {
            _fileExtensions = new HashSet<string>(fileExtensions.Select(e => e.StartsWith('.') ? e : "." + e), StringComparer.OrdinalIgnoreCase);
            ErrorMessage = $"Разрешённые расширения: {string.Join(", ", fileExtensions)}";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is not IFormFile file) return new ValidationResult("Некорректный тип файла");

            if (!_fileExtensions.Contains(Path.GetExtension(file.FileName))) return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}