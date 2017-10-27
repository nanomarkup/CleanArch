using AutoMapper;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;

namespace Core.Mapping
{
    public class GatewaysProfile : Profile
    {
        public GatewaysProfile()
        {
            // The destination type should be Gateway DTO for all maps
            CreateMap<DtoMessageReadByDateInteractor, DtoMessageQueryGateway>();

            CreateMap<IUserEntity, DtoUserInfoGateway>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Identity.Id))
                .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.Identity.Created))
                .ForMember(dest => dest.Modified, opts => opts.MapFrom(src => src.Identity.Modified));

            CreateMap<IUserEntity, DtoUserModifiedGateway>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Identity.Id))
                .ForMember(dest => dest.Modified, opts => opts.MapFrom(src => src.Identity.Modified));

            CreateMap < IMessageEntity, DtoMessageInfoGateway>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Identity.Id))
                .ForMember(dest => dest.Created, opts => opts.MapFrom(src => src.Identity.Created))
                .ForMember(dest => dest.Modified, opts => opts.MapFrom(src => src.Identity.Modified));
        }
    }
}
