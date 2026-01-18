using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class AuthTrykeyDto
    {
        [Required(ErrorMessage = "Укажите ключ")]
        public required string Key { get; set; }
    }
}