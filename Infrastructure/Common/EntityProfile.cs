using AutoMapper;
using Core.Gateways;
using Infrastructure.Entities;

namespace Infrastructure.Common
{
    public class EntityProfile : Profile
    {
        public EntityProfile()
        {
            // The destination type should be Entity object for all maps
            CreateMap<DtoMessageInfoGateway, MessageEntity>();
        }
    }
}
