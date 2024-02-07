using BasicInfoManage.Application.Entities;

namespace BasicInfoManage.Application.Interfaces
{
    public interface ICountryInfoService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        
        Task<(IEnumerable<Country>, int totalItemCount)> GetCountriesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Country?> GetCountryAsync(int countryId, bool includeAirports);
        Task<bool> CountryExistsAsync(int countryId);
        Task<bool> CountryNameMatchesCountryId(string? countryName, int countryId);

        Task<IEnumerable<Airport>> GetAirportsForCountryAsync(int countryId);
        Task<Airport?> GetAirportForCountryAsync(int countryId, int airportId);
        Task AddAirportForCountryAsync(int countryId, Airport airport);
        void DeleteAirport(Airport airport);
        
        Task<bool> SaveChangesAsync();
        /**/
    }
}
