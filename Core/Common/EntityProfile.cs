using AutoMapper;
using Core.Entities;
using Core.Gateways;
using Core.Interactors;

namespace Core.Common
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            // The destination type should be Entity DTO for all maps
            CreateMap<DtoUserCreateInteractor, DtoUserEntity>();            
            CreateMap<DtoMessageSendInteractor, DtoMessageEntity>();

            CreateMap<DtoUserInfoGateway, DtoUserIdentityEntity>()
                .ForMember(dest => dest.Identity, opts => opts.MapFrom(src => new DtoIdentityEntity()
                {
                    Id = src.Id,
                    Created = src.Created,
                    Modified = src.Modified
                }));

            CreateMap<DtoMessageInfoGateway, DtoMessageIdentityEntity>()
                .ForMember(dest => dest.Identity, opts => opts.MapFrom(src => new DtoIdentityEntity()
                {
                    Id = src.Id,
                    Created = src.Created,
                    Modified = src.Modified
                }));            
        }
    }
}
