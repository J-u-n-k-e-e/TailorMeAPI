using Microsoft.EntityFrameworkCore;
using TailorMeWebApi.Models;

namespace TailorMeWebApi.GraphQL.Types
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
    }
}
