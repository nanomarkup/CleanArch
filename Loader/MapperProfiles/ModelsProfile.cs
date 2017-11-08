using AutoMapper;
using Core.Interactors;
using Core.Models;

namespace Loader.MapperProfiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            // The destination type should be Model object for all maps
            CreateMap<DtoMessageInteractorSend, MessageModel>();
            CreateMap<DtoUserInteractorCreate, UserModel>();
        }
    }
}
