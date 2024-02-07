using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Text.Json;
using BasicInfoManage.Application.Interfaces;
using BasicInfoManage.Application.Services;
using BasicInfoManage.Application.Dtos;
using BasicInfoManage.Application.Entities;


namespace BasicInfoManage.Controllers
{
    [Route("api/v{version:apiVersion}/countries/{countryId}/airports")]
    //[Authorize(Policy = "MustBeFromAntwerp")]
    [ApiVersion("2.0")]
    [ApiController]
    public class AirportsController : Controller
    {
        //private readonly ILogger<AirportsController> _logger;

        private readonly ICountryInfoService _countryInfoService;
        private readonly IMapper _mapper;

        public AirportsController(ICountryInfoService countryInfo,
            IMapper mapper)
        {
            _countryInfoService = countryInfo ??
                throw new ArgumentNullException(nameof(countryInfo));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AirportDto>>> GetAirports(
            int countryId)
        {

            if (!await _countryInfoService.CountryExistsAsync(countryId))
            {
                //_logger.LogInformation($"CountryId {countryId} wasn't found.");
                return NotFound();
            }

            var pointsOfInterestForCity = await _countryInfoService.GetAirportsForCountryAsync(countryId);

            return Ok(_mapper.Map<IEnumerable<AirportDto>>(pointsOfInterestForCity));
        }

        [HttpGet("{airportId}", Name = "GetAirport")]
        public async Task<ActionResult<AirportDto>> GetAirport(
            int countryId, int airportId)
        {
            if (!await _countryInfoService.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var airport = await _countryInfoService
                .GetAirportForCountryAsync(countryId, airportId);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AirportDto>(airport));
        }


        [HttpPost]
        public async Task<ActionResult<AirportDto>> CreateAirport(
           int countryId,
           AirportCreationDto airportCreation)
        {
            if (!await _countryInfoService.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var finalAirport = _mapper.Map<Application.Entities.Airport>(airportCreation);

            await _countryInfoService.AddAirportForCountryAsync(
                countryId, finalAirport);

            await _countryInfoService.SaveChangesAsync();

            var createdAirportToResult =
                _mapper.Map<AirportDto>(finalAirport);

            return CreatedAtRoute("GetAirport",
                 new
                 {
                     countryId = countryId,
                     airportId = createdAirportToResult.Id
                 },
                 createdAirportToResult);
        }

        [HttpPut("{airportId}")]
        public async Task<ActionResult> UpdatePointOfInterest(int countryId, int airportId,
             AirportUpdateDto airportUpdate)
        {
            if (!await _countryInfoService.CountryExistsAsync(countryId))
            {
                return NotFound();
            }

            var airportEntity = await _countryInfoService
                .GetAirportForCountryAsync(countryId, airportId);
            if (airportEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(airportUpdate, airportEntity);

            await _countryInfoService.SaveChangesAsync();

            return NoContent();
        }

    }
}
