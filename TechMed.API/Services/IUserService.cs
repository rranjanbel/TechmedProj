namespace TechMed.API.Services
{
    public interface IUserService
    {
        Task<bool> IsAnExistingUser(string userEmail);
        Task<bool> IsValidUserCredentials(string userName, string password);
    }
}
