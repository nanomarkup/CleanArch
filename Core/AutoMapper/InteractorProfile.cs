using System;
using AutoMapper;
using Core.Gateways;
using Core.Interactors;

namespace Core.AutoMapper
{
    public class InteractorProfile : Profile
    {
        public InteractorProfile()
        {
            // The destination type should be Interactor DTO (DtoI) for all maps
            CreateMap<DtoGUserInfo, DtoIUserInfo>();
            CreateMap<DtoGMessageInfo, DtoIMessageInfo>();            

            CreateMap<DtoIMessageRead, DtoIMessageReadByDate>()
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => DateTime.MinValue));            
        }
    }
}
