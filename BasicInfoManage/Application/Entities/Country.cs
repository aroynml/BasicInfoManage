using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BasicInfoManage.Application.Entities
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(3)]
        public string CountryCodeA3 { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public ICollection<Airport> Airports { get; set; }
               = new List<Airport>();

        public Country(string name, string countryCodeA3)
        {
            Name = name;
            CountryCodeA3 = countryCodeA3;
        }

        public Country(string name, string countryCodeA3, string description)
        {
            Name = name;
            CountryCodeA3 = countryCodeA3;
            Description = description;
        }
    }
}
