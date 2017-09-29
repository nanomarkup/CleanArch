using AutoMapper;
using Core.Gateways;
using Infrastructure.Entities;

namespace Infrastructure.Common
{
    public class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            // The destination type should be Gateway DTO for all maps
            CreateMap<MessageEntity, DtoMessageInfoGateway>();
        }
    }
}
