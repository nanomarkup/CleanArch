using AutoMapper;
using Core.Models;
using Core.Interactors;
using System;

namespace Core.Mapping
{
    public class InteractorsProfile : Profile
    {
        public InteractorsProfile()
        {
            // The destination type should be Interactor DTO for all maps
            CreateMap<UserModel, DtoUserInfoInteractor>();
            CreateMap<MessageModel, DtoMessageInfoInteractor>();            
            CreateMap<DtoMessageReadInteractor, DtoMessageReadByDateInteractor>()
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => DateTime.MinValue));            
        }
    }
}
