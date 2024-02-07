using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Text.Json;
using BasicInfoManage.Application.Interfaces;
using BasicInfoManage.Application.Services;
using BasicInfoManage.Application.Dtos;

namespace BasicInfoManage.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        //private readonly ILogger<CountriesController> _logger;

        private readonly ICountryInfoService _countryInfoService;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;
        
        public CountriesController(ICountryInfoService countryInfo,
            IMapper mapper)
        {
            _countryInfoService = countryInfo ??
                throw new ArgumentNullException(nameof(countryInfo));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        
        /// <summary>
        /// To get countries by filter and pagesize
        /// </summary>
        /// <param name="name"></param>
        /// <param name="searchQuery"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }

            var (countries, totalItemCount) = await _countryInfoService
                .GetCountriesAsync(name, searchQuery, pageNumber, pageSize);
            
            var paginationMetadata = new
            {
                totalCount = totalItemCount,
                pageSize = pageSize,
                currentPage = pageNumber,
                totalPages = (int)Math.Ceiling(totalItemCount / (double)pageSize)
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CountryDto>>(countries));
        }

        /// <summary>
        /// Get a city by id
        /// </summary>
        /// <param name="id">id of the country</param>
        /// <param name="includeAirports">to include the airports</param>
        /// <returns>An IActionResult</returns>
        /// <response code="200">Returns the requested country</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCountry(
            int id, bool includeAirports = false)
        {
            var country = await _countryInfoService.GetCountryAsync(id, includeAirports);
            if (country == null)
            {
                return NotFound();
            }

            if (includeAirports)
            {
                return Ok(_mapper.Map<CountryInfoDto>(country));
            }

            return Ok(_mapper.Map<CountryDto>(country));
        }
        /**/
    }
}
