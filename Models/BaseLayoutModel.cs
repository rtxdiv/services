namespace aspnet1.Models
{
    public abstract class BaseLayoutModel
    {
        public LayoutData Layout { get; set; } = new();
    }

    public class LayoutData
    {
        public bool ShowRequests { get; set; } = false;
        public bool ShowNotification { get; set; } = false;
    }
}