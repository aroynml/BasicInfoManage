namespace BasicInfoManage.Application.Dtos
{
    public class AirportDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IataCode { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
