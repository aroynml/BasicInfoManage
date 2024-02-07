using AutoMapper;
namespace BasicInfoManage.Application.Mapping
{
    public class CountryMapping : Profile
    {
        public CountryMapping()
        {
            CreateMap<Entities.Country, Dtos.CountryDto>();
            CreateMap<Entities.Country, Dtos.CountryInfoDto>();
        }
    }
}
