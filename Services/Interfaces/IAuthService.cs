using System.Runtime.InteropServices;

namespace services.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Validation> ValidateUser(HttpContext context, [Optional] VParams vparams);
    }

    public class Validation
    {
        public required bool Valide { get; set; }
        public required string? UserId { get; set; }
        public required int RequestsCount { get; set; }
    }
    public class VParams
    {
        public bool NewId { get; set; } = false;
    }
    public class NotValidException : Exception {}
}