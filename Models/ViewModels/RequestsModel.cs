using services.Entity;

namespace services.Models.ViewModels
{
    public class RequestsModel : BaseLayoutModel
    {
        public List<Request> Requests { get; set; } = [];
        public bool Admin { get; set; } = false;
    }
}