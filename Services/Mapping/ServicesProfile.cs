using AutoMapper;
using Core.Interactors;

namespace Services.Mapping
{
    public class ServicesProfile : Profile
    {
        public ServicesProfile()
        {            
            // Destination is Interactor DTO, input
            CreateMap<DtoServiceMessageSend, DtoMessageSendInteractor>();
            CreateMap<DtoServiceMessageRead, DtoMessageReadInteractor>();
            CreateMap<DtoServiceMessageReadById, DtoMessageReadByIdInteractor>();
            CreateMap<DtoServiceMessageReadByDate, DtoMessageReadByDateInteractor>();

            CreateMap<DtoServiceUserId, DtoUserIdInteractor>();
            CreateMap<DtoServiceUserCreate, DtoUserCreateInteractor>();
            CreateMap<DtoServiceUserModify, DtoUserModifyInteractor>();

            // Destination is Service DTO, output
            CreateMap<DtoMessageIdInteractor, DtoServiceMessageId>();
            CreateMap<DtoMessageInfoInteractor, DtoServiceMessageInfo>();

            CreateMap<DtoUserIdInteractor, DtoServiceUserId>();
            CreateMap<DtoUserInfoInteractor, DtoServiceUserInfo>();
        }
    }
}
