using System.ComponentModel.DataAnnotations;

namespace services.Models.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
            ErrorMessage = $"Максимальный размер файла: {_maxFileSize / 1024 / 1024 } Мб";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (value is not IFormFile file) return new ValidationResult("Некорректный тип файла");

            if (file.Length > _maxFileSize) return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}