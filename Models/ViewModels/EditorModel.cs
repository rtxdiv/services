namespace services.Models.ViewModels
{
    public class EditorModel
    {
        public int ServiceId { get; set; } = -1;
        public string Image { get; set; } = "addimage.jpg";
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Requirements { get; set; } = String.Empty;
        public bool New { get; set; } = false;
    }
}