using Microsoft.EntityFrameworkCore;
using BasicInfoManage.Data;
using BasicInfoManage.Application.Interfaces;
using BasicInfoManage.Application.Entities;

namespace BasicInfoManage.Application.Services
{
    public class CountryInfoService : ICountryInfoService
    {
        private readonly CountryInfoContext _context;

        public CountryInfoService(CountryInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        }
        
        public async Task<bool> CountryExistsAsync(int countryId)
        {
            return await _context.Countries.AnyAsync(c => c.Id == countryId);
        }

        public async Task<bool> CountryNameMatchesCountryId(
            string? countryName, int countryId)
        {
            return await _context.Countries.AnyAsync(c => c.Id == countryId && c.Name == countryName);
        }

        public async Task<(IEnumerable<Country>, int)> GetCountriesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Countries as IQueryable<Country>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(
                    a => a.Name.Contains(searchQuery)
                    || (a.Description != null && a.Description.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, totalItemCount);
        }


        public async Task<Country?> GetCountryAsync(
            int countryId, bool includeAirports)
        {
            if (includeAirports)
            {
                return await _context.Countries.Include(c => c.Airports)
                    .Where(c => c.Id == countryId).FirstOrDefaultAsync();
            }

            return await _context.Countries
                  .Where(c => c.Id == countryId).FirstOrDefaultAsync();
        }

        public async Task<Airport?> GetAirportForCountryAsync(
            int countryId, int airportId)
        {
            return await _context.Airports
               .Where(p => p.CountryId == countryId && p.Id == airportId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Airport>> GetAirportsForCountryAsync(
            int countryId)
        {
            return await _context.Airports
                           .Where(p => p.CountryId == countryId).ToListAsync();
        }

        public async Task AddAirportForCountryAsync(int countryId,
            Airport airport)
        {
            var country = await GetCountryAsync(countryId, false);
            if (country != null)
            {
                country.Airports.Add(airport);
            }
        }

        public void DeleteAirport(Airport airport)
        {
            _context.Airports.Remove(airport);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        /**/
    }
}
