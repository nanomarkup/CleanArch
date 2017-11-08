using AutoMapper;
using Core.Interactors;

namespace Services.Mapping
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {            
            // Destination is Interactor DTO, input
            CreateMap<DtoServiceMessageSend, DtoMessageInteractorSend>();
            CreateMap<DtoServiceMessageRead, DtoMessageInteractorRead>();
            CreateMap<DtoServiceMessageReadById, DtoMessageInteractorReadById>();
            CreateMap<DtoServiceMessageReadByDate, DtoMessageInteractorReadByDate>();

            CreateMap<DtoServiceUserId, DtoUserInteractorId>();
            CreateMap<DtoServiceUserCreate, DtoUserInteractorCreate>();
            CreateMap<DtoServiceUserModify, DtoUserInteractorModify>();

            // Destination is Service DTO, output
            CreateMap<DtoMessageInteractorId, DtoServiceMessageId>();
            CreateMap<DtoMessageInteractorInfo, DtoServiceMessageInfo>();

            CreateMap<DtoUserInteractorId, DtoServiceUserId>();
            CreateMap<DtoUserInteractorInfo, DtoServiceUserInfo>();
        }
    }
}
