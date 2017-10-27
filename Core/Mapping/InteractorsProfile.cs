using System;
using AutoMapper;
using Core.Gateways;
using Core.Interactors;

namespace Core.Mapping
{
    public class InteractorsProfile : Profile
    {
        public InteractorsProfile()
        {
            // The destination type should be Interactor DTO for all maps
            CreateMap<DtoUserInfoGateway, DtoUserInfoInteractor>();
            CreateMap<DtoMessageInfoGateway, DtoMessageInfoInteractor>();            

            CreateMap<DtoMessageReadInteractor, DtoMessageReadByDateInteractor>()
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => DateTime.MinValue));            
        }
    }
}
