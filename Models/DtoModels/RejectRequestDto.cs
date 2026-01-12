using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class RejectRequestDto
    {
        [Required(ErrorMessage = "Не указан Id услуги")]
        public required int RequestId { get; set; }
    }
}