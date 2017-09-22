using System;
using AutoMapper;
using Core.Interactors;

namespace Core.AutoMapper
{
    public class InteractorProfile : Profile
    {
        public InteractorProfile()
        {
            // The destination type should be Interactor DTO (DtoI) for all maps
            CreateMap<DtoIMessageReadRequest, DtoIMessageReadByDateRequest>()
                .ForMember(dest => dest.Start, opts => opts.MapFrom(src => DateTime.MinValue));
        }
    }
}
