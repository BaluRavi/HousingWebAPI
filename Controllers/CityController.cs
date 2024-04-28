using HousingWebAPI.Data;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Get()
        {
            var cities = appDataContext.Cities;
            return Ok(cities);
        }

        //test method to pass get method with paramaters

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "Chennai";
        }
    }

}
