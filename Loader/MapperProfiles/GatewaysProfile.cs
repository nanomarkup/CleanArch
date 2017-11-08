using AutoMapper;
using Core.Gateways;
using Core.Interactors;

namespace Loader.MapperProfiles
{
    public class GatewaysProfile : Profile
    {
        public GatewaysProfile()
        {
            // The destination type should be Gateway DTO for all maps
            CreateMap<DtoMessageInteractorReadByDate, DtoMessageGatewayQuery>();            
        }
    }
}
