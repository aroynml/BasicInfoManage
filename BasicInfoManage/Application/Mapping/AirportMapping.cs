using AutoMapper;
namespace BasicInfoManage.Application.Mapping
{
    public class AiportMapping : Profile
    {
        public AiportMapping()
        {
            CreateMap<Entities.Airport, Dtos.AirportDto>();
            CreateMap<Entities.Airport, Dtos.AirportCreationDto>();
            CreateMap<Entities.Airport, Dtos.AirportUpdateDto>();
            CreateMap<Dtos.AirportCreationDto, Entities.Airport>();
            CreateMap<Dtos.AirportUpdateDto, Entities.Airport>();
        }
    }
}
