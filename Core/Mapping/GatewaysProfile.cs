using AutoMapper;
using Core.Models;
using Core.Gateways;
using Core.Interactors;

namespace Core.Mapping
{
    public class GatewaysProfile : Profile
    {
        public GatewaysProfile()
        {
            // The destination type should be Gateway DTO for all maps
            CreateMap<IUserModel, DtoUserInfoGateway>();
            CreateMap<IMessageModel, DtoMessageInfoGateway>();
            CreateMap<DtoMessageReadByDateInteractor, DtoMessageQueryGateway>();            
        }
    }
}
