using services.Entity;

namespace services.Models.ViewModels
{
    public class HomeModel : BaseLayoutModel
    {
        public List<Service> Services { get; set; } = [];
        public bool Admin { get; set; } = false;
    }
    // создание МОДЕЛИ (расширяющая модель BaseLayout) для передачи в VIEW
}
