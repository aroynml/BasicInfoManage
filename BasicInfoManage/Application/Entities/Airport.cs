using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasicInfoManage.Application.Entities
{
    public class Airport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string IataCode { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }


        [ForeignKey("CountryId")]
        public Country? Country { get; set; }
        public int CountryId { get; set; }

        public Airport(string name, string iataCode)
        {
            Name = name;
            IataCode = iataCode;
        }
        public Airport(int countryId, string name, string iataCode)
        {
            Name = name;
            IataCode = iataCode;
            CountryId = countryId;
        }
    }
}
