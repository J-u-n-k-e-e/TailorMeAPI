using TailorMeWebApi.Interfaces;
using TailorMeWebApi.Models;

namespace TailorMeWebApi.GraphQL.Queries
{
    public class UserQueries
    {
        public async Task<List<User>> GetUsers([Service] IUserInterface userInterface)
        {
            return await userInterface.GetUsers();
        }
    }
}
