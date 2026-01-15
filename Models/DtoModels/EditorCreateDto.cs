namespace services.Models.DtoModels
{
    public class EditorCreateDto
    {
        public required IFormFile Image { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Requirements { get; set; }
    }
}