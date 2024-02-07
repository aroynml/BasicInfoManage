using Microsoft.EntityFrameworkCore;
using BasicInfoManage.Application.Entities;

namespace BasicInfoManage.Data
{
    public class CountryInfoContextSeed
    {
        public static async Task SeedAsync(CountryInfoContext countryInfoContext,
                ILogger logger,
                int retry = 0)
        {
            var retryForAvailability = retry;

            try
            {
                if (!await countryInfoContext.Countries.AnyAsync())
                {
                    await countryInfoContext.Countries.AddRangeAsync(
                        GetPreconfiguredCountries());

                    await countryInfoContext.SaveChangesAsync();
                }

                if (!await countryInfoContext.Airports.AnyAsync())
                {
                    await countryInfoContext.Airports.AddRangeAsync(
                        GetPreconfiguredAirports());

                    await countryInfoContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(countryInfoContext, logger, retryForAvailability);
                throw;
            }
        }

        private static IEnumerable<Country> GetPreconfiguredCountries()
        {
            return new List<Country>
            {
                new Country ("Bahamas","BHS") ,
                new Country ("Bahrain","BHR") ,
                new Country ("Bangladesh","BGD"),
                new Country ("Barbados","BRB") ,
                new Country ("Belarus","BLR") ,
                new Country ("Belgium","BEL") ,
                new Country ("Belize","BLZ") ,
                new Country ("Benin","BEN") ,
                new Country ("Bhutan","BTN"),
                new Country ("Bolivia","BOL"),
                new Country ("Bosnia and Herzegovina","BIH"),
                new Country ("Botswana","BWA") ,
                new Country ("Brazil","BRA") ,
                new Country ("Brunei Darussalam","BRN") ,
                new Country ("Bulgaria","BGR"),
                new Country ("Burundi","BDI"),
                /**/
            };
        }

        private static IEnumerable<Airport> GetPreconfiguredAirports()
        {
            return new List<Airport>
            {
                new Airport (2, "Bahrain, Bahrain International","BAH") {Id = 11001},
                new Airport (3, "Chittagong, Bangladesh Patenga","CGP") {Id = 12001},
                new Airport (3, "Dhaka, Bangladesh Zia International Airport","DAC") {Id = 12002},
                new Airport (3, "Saidpur, Bangladesh","SPD") {Id = 12003},
                new Airport (6, "Antwerp, Belgium Deurne","ANR") {Id = 15001},
                new Airport (6, "Brussels, Belgium National","BRU") {Id = 15002},
                new Airport (6, "Liege, Belgium Bierset","LGG") {Id = 15003},
                new Airport (13, "Belem, Para, Brazil Val De Cans","BEL") {Id = 22001},

                /**/
            };
        }
    }
}
