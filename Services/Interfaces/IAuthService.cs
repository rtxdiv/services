namespace services.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUserId(string userId);
    }
}