using AutoMapper;
using CustomerApplication.Domain.Entities;

namespace CustomerApplication.Domain.DTO
{
    public class SalesProfile: Profile
    {
        public SalesProfile()
        {
            CreateMap<Sales, SalesDTO>()
                .ForMember(dest => dest.SalesId,
                    opt => opt.MapFrom(src => src.SalesId))
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Phonenumber,
                    opt => opt.MapFrom(src => src.Phonenumber))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email));


            CreateMap<SalesCreateDTO, Sales>()
                .ForMember(dest => dest.FullName,                
                    opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Phonenumber,
                    opt => opt.MapFrom(src => src.Phonenumber))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email));
        }
    }
}
