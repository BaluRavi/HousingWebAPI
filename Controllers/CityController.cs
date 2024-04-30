using HousingWebAPI.Data.Repo;
using HousingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository repo;
        public CityController(ICityRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cities = await repo.GetCitiesAsync();

            return Ok(cities);
        }

        // To add new city in the database

        //api/City/add?cityname=Chennai

        //[HttpPost("add")]
        //[HttpPost("add/{cityName}")]
        //public async Task<IActionResult> AddCity(string cityName)
        //{
        //    City city = new City();
        //    city.Name = cityName;

        //    await appDataContext.Cities.AddAsync(city);
        //    await appDataContext.SaveChangesAsync();
        //    return Ok(city);
        //}

        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            repo.AddCity(city);
            await repo.SaveCityAsnc();
            return StatusCode(201);
        }

        //api/City/delete/1

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            repo.DeleteCity(id);
            await repo.SaveCityAsnc();
            return Ok(id);
        }
    }
}
