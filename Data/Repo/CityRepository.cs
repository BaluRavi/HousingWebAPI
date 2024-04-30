using HousingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HousingWebAPI.Data.Repo
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDataContext appDataContext;

        public CityRepository(AppDataContext appDataContext)
        {
            this.appDataContext = appDataContext;
        }
        public void AddCity(City city)
        {
            appDataContext.Cities.AddAsync(city);
        }

        public void DeleteCity(int cityId)
        {
            var city = appDataContext.Cities.Find(cityId);

            if (city != null)
            {
                appDataContext.Cities.Remove(city);
            }
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await appDataContext.Cities.ToListAsync();
        }

        public async Task<bool> SaveCityAsnc()
        {
            return await appDataContext.SaveChangesAsync() > 0;
        }
    }
}
