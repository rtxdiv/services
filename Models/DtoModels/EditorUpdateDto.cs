namespace services.Models.DtoModels
{
    public class EditorUpdateDto
    {
        public IFormFile? Image { get; set; } = null;
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Requirements { get; set; }
    }
}