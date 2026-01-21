using System.Runtime.InteropServices;
using services.Entity;

namespace services.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Validation> ValidateUser(HttpContext context, [Optional] VParams vparams);
        string Hash(string row);
        bool VerifyHash(string hash);
        bool VerifyKey(string key);
    }

    public class Validation
    {
        public required bool Valide { get; set; }
        public required string? UserId { get; set; }
        public required int RequestsCount { get; set; }
        public int NotiCount { get; set; } = 0;
    }
    public class VParams
    {
        public bool NewId { get; set; } = false;
    }
    public class NotValidException : Exception {}
}