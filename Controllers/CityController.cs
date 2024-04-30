using HousingWebAPI.Data;
using HousingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HousingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly AppDataContext appDataContext;

        public CityController(AppDataContext appDataContext)
        {
            this.appDataContext = appDataContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cities = await appDataContext.Cities.ToListAsync();

            return Ok(cities);
        }

        // To add new city in the database

        //api/City/add?cityname=Chennai

        [HttpPost("add")]
        [HttpPost("add/{cityName}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new City();
            city.Name = cityName;

            await appDataContext.Cities.AddAsync(city);
            await appDataContext.SaveChangesAsync();
            return Ok(city);
        }

        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            await appDataContext.Cities.AddAsync(city);
            await appDataContext.SaveChangesAsync();
            return Ok(city);
        }

        //api/City/delete/1

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await appDataContext.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (city != null)
            {
                appDataContext.Cities.Remove(city);
                await appDataContext.SaveChangesAsync();
                return Ok(id);
            }
            else
            {
                return BadRequest("Cant able to delete city");
            }
        }
    }
}
