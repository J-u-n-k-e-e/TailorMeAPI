using Microsoft.EntityFrameworkCore;
using TailorMeWebApi.GraphQL.Types;

namespace TailorMeWebApi
{
    public class SeedData
    {
        public static void Initialise (IServiceProvider serviceProvider)
        {
            using (var context = new DbContextClass(
                serviceProvider.GetRequiredService<DbContextOptions<DbContextClass>>()))
            {
                context.User.AddRange(
                    new Models.User
                    {
                        UserId = "1",
                        Username = "JudeTest",
                        ChestMeasurement = 36,
                        WaistMeasurement = 24,
                        HipMeasurement = 26
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
