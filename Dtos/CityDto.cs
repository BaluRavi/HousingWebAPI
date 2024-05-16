using System.ComponentModel.DataAnnotations;

namespace HousingWebAPI.Dtos
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "City Name is mandatory Field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Only Numbers are not allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country Name is mandatory for City")]
        public string Country { get; set; }
    }
}
