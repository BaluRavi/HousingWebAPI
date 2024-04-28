using HousingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HousingWebAPI.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
    }
}
