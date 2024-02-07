using BasicInfoManage.Application.Entities;

namespace BasicInfoManage.Application.Dtos
{
    public class CountryInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryCodeA3 { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int AirportsCount
        {
            get
            {
                return Airports.Count;
            }
        }

        public ICollection<AirportDto> Airports { get; set; }
            = new List<AirportDto>();
    }
}
