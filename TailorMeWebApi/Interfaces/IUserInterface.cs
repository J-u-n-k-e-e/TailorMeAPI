using TailorMeWebApi.Models;

namespace TailorMeWebApi.Interfaces
{
    public interface IUserInterface
    {
        Task<List<User>> GetUsers();
        Task<bool> CreateUserAsync(CreateUserModel userModel);
    }
}
