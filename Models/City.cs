namespace HousingWebAPI.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string LastUpdatedBy { get; set; }
        public string Country { get; set; }
    }
}
