using BasicInfoManage.Application.Entities;

namespace BasicInfoManage.Application.Dtos
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CountryCodeA3 { get; set; } = string.Empty;
        public string? Description { get; set; }
        
    }
}
