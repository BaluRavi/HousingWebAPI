using HousingWebAPI.Models;

namespace HousingWebAPI.Data.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int cityId);
        Task<City> GetCityById(int cityId);

    }
}
