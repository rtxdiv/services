using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class OrderSendDto
    {
        [Required]
        public required int ServiceId { get; set; }

        [MinLength(30, ErrorMessage = "Используйте минимум 30 символов")]
        public required string Query { get; set; }

        [MinLength(1, ErrorMessage = "Укажите ваши контакты")]
        public required string Contact { get; set; }
    }
}