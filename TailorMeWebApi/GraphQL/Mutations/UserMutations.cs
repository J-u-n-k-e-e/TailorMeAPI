using TailorMeWebApi.Interfaces;
using TailorMeWebApi.Models;

namespace TailorMeWebApi.GraphQL.Mutations
{
    public class UserMutations
    {
        public async Task<bool> CreateUser([Service] IUserInterface userInterface, CreateUserModel userModel)
        {
            return await userInterface.CreateUserAsync(userModel);
        }
    }
}
