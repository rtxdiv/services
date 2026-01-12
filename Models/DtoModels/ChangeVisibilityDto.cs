using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class ChangeVisibilityDto
    {
        [Required(ErrorMessage = "Не указан Id услуги")]
        public required int ServiceId { get; set; }
    }
}