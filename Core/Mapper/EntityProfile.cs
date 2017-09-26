using AutoMapper;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;

namespace Core.Mapper
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            // The destination type should be Entity DTO (DtoE) for all maps
            CreateMap<DtoIUserCreate, DtoEUser>();            
            CreateMap<DtoIMessageSend, DtoEMessage>();

            CreateMap<DtoGUserInfo, DtoEUserIdentity>()
                .ForMember(dest => dest.Identity, opts => opts.MapFrom(src => new DtoEIdentity()
                {
                    Id = src.Id,
                    Created = src.Created,
                    Modified = src.Modified
                }));

            CreateMap<DtoGMessageInfo, DtoEMessageIdentity>()
                .ForMember(dest => dest.Identity, opts => opts.MapFrom(src => new DtoEIdentity()
                {
                    Id = src.Id,
                    Created = src.Created,
                    Modified = src.Modified
                }));            
        }
    }
}
