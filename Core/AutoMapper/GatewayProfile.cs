using AutoMapper;
using Core.Entities;
using Core.Gateways;

namespace Core.AutoMapper
{
    public class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            // The destination type should be Gateway DTO (DtoG) for all maps
            CreateMap<IUserEntity, DtoGUserInfo>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Identity.Id))
                .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.Identity.Created))
                .ForMember(dest => dest.Modified, opts => opts.MapFrom(src => src.Identity.Modified));
        }
    }
}
