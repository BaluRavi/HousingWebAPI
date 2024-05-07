using HousingWebAPI.Data.Interfaces;
using HousingWebAPI.Dtos;
using HousingWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HousingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public CityController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();

            var citiesDTO = from c in cities
                            select new CityDto { Id = c.Id, Name = c.Name };

            return Ok(citiesDTO);
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
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = new City
            {
                Name = cityDto.Name,
                LastUpdatedBy = 2,
                LastUpdatedOn = DateTime.Now
            };

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        //api/City/delete/1

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            uow.CityRepository.DeleteCity(id);
            await uow.SaveAsync();
            return Ok(id);
        }
    }
}
