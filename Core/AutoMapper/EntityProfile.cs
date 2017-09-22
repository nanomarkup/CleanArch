using AutoMapper;
using Core.Entities;
using Core.Interactors;

namespace Core.AutoMapper
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            // The destination type should be Entity DTO (DtoE) for all maps
            CreateMap<DtoIUserCreate, DtoEUser>();
            CreateMap<DtoIMessageSendRequest, DtoEMessage>();
        }
    }
}
