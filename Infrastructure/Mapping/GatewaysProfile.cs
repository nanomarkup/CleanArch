using AutoMapper;
using Core.Gateways;
using Infrastructure.Models;

namespace Infrastructure.Mapping
{
    public class GatewaysProfile : Profile
    {
        public GatewaysProfile()
        {
            // The destination type should be Gateway DTO for all maps
            CreateMap<MessageModel, DtoMessageInfoGateway>();
        }
    }
}
