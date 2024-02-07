using System.ComponentModel.DataAnnotations;

namespace BasicInfoManage.Application.Dtos
{
    public class AirportCreationDto
    {
        [Required(ErrorMessage = "You should provide Name ")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "You should provide IATA ")]
        [MaxLength(3)]
        public string IataCode { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
