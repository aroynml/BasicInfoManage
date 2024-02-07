using Microsoft.EntityFrameworkCore;
using BasicInfoManage.Application.Entities;
using System.Reflection;

namespace BasicInfoManage.Data
{
    public class CountryInfoContext : DbContext
    {
        public CountryInfoContext() { }

        public CountryInfoContext(DbContextOptions<CountryInfoContext> options)
            : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("CountryInfo");
        }
        /**/
    }
}
