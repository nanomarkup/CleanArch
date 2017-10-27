using AutoMapper;
using Core.Gateways;
using Infrastructure.Models;

namespace Infrastructure.Mapping
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            // The destination type should be Model object for all maps
            CreateMap<DtoMessageInfoGateway, MessageModel>();
        }
    }
}
