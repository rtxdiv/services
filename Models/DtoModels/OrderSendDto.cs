using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class OrderSendDto
    {
        [Required(ErrorMessage = "Не указан Id услуги")]
        public required int ServiceId { get; set; }

        [MinLength(100, ErrorMessage = "Используйте минимум 100 символов")]
        [MaxLength(2000, ErrorMessage = "Используйте не более 2000 символов")]
        public required string Query { get; set; }

        [MinLength(1, ErrorMessage = "Укажите ваши контакты")]
        public required string Contact { get; set; }
    }
}