using AutoMapper;
using HousingWebAPI.Data.Interfaces;
using HousingWebAPI.Dtos;
using HousingWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HousingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public CityController(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var cities = await uow.CityRepository.GetCitiesAsync();

            var citiesDTO = mapper.Map<IEnumerable<CityDto>>(cities);

            //var citiesDTO = from c in cities
            //                select new CityDto { Id = c.Id, Name = c.Name };

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
            //var city = new City
            //{
            //    Name = cityDto.Name,
            //    LastUpdatedBy = 2,
            //    LastUpdatedOn = DateTime.Now
            //};

            var city = mapper.Map<City>(cityDto);

            city.LastUpdatedBy = "Balu";
            city.LastUpdatedOn = DateTime.Now;

            uow.CityRepository.AddCity(city);
            await uow.SaveAsync();
            return StatusCode(201);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityDto cityDto)
        {
            var cityFromDB = await uow.CityRepository.GetCityById(id);

            cityFromDB.LastUpdatedBy = "Balu";
            cityFromDB.LastUpdatedOn = DateTime.Now;

            mapper.Map(cityDto, cityFromDB);
            await uow.SaveAsync();

            return StatusCode(200);
        }


        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateCityPatch(int id, JsonPatchDocument<City> cityToPatch)
        {
            try
            {
                var cityFromDB = await uow.CityRepository.GetCityById(id);

                cityFromDB.LastUpdatedBy = "Balu";
                cityFromDB.LastUpdatedOn = DateTime.Now;

                cityToPatch.ApplyTo(cityFromDB, ModelState);

                await uow.SaveAsync();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(400);
            }
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
