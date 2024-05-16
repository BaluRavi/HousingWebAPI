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
            throw new UnauthorizedAccessException();

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            //try
            //{
            if (id != cityDto.Id)
                return BadRequest("Update not allowed");

            var cityFromDB = await uow.CityRepository.GetCityById(id);

            if (cityFromDB == null)
                return BadRequest("Update not allowed");

            cityFromDB.LastUpdatedBy = "Balu";
            cityFromDB.LastUpdatedOn = DateTime.Now;

            throw new Exception("some error occured");

            mapper.Map(cityDto, cityFromDB);
            await uow.SaveAsync();

            return StatusCode(200);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, "some unknown error occured for testing balu");
            //}
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
