using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class AcceptRequestDto
    {
        [Required]
        public required int RequestId { get; set; }
    }
}