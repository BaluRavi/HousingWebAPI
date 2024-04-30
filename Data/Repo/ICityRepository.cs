﻿using HousingWebAPI.Models;

namespace HousingWebAPI.Data.Repo
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int cityId);
        Task<bool> SaveCityAsnc();
    }
}
