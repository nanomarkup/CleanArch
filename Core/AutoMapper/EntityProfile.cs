using AutoMapper;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;

namespace Core.AutoMapper
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            // The destination type should be Entity DTO (DtoE) for all maps
            CreateMap<DtoIUserCreate, DtoEUser>();            
            CreateMap<DtoIMessageSend, DtoEMessage>();

            CreateMap<DtoGUserInfo, DtoEUserIdentity>()
                .ForMember(dest => dest.Identity.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identity.Created, opts => opts.MapFrom(src => src.Created))
                .ForMember(dest => dest.Identity.Modified, opts => opts.MapFrom(src => src.Modified));

            CreateMap<DtoGMessageInfo, DtoEMessageIdentity>()
                .ForMember(dest => dest.Identity.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identity.Created, opts => opts.MapFrom(src => src.Created))
                .ForMember(dest => dest.Identity.Modified, opts => opts.MapFrom(src => src.Modified));
        }
    }
}
