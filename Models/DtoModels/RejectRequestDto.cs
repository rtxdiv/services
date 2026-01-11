using System.ComponentModel.DataAnnotations;

namespace services.Models.DtoModels
{
    public class RejectRequestDto
    {
        [Required]
        public required int RequestId { get; set; }
    }
}