using AutoMapper;
using CustomerApplication.Domain.Entities;

namespace CustomerApplication.Domain.DTO
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.CustomerId,
                        opt => opt.MapFrom(src => src.CustomerId))
                    .ForMember(dest => dest.FullName,
                        opt => opt.MapFrom(src => src.FullName))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dest => dest.Address,
                        opt => opt.MapFrom(src => src.Address))
                    .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.Sales,
                        opt => opt.MapFrom(src => src.Sales));


            CreateMap<CustomerCreateDTO, Customer>()
                .ForMember(dest => dest.FullName,
                        opt => opt.MapFrom(src => src.FullName))
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.PhoneNumber,
                        opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dest => dest.Address,
                        opt => opt.MapFrom(src => src.Address))
                    .ForMember(dest => dest.Email,
                        opt => opt.MapFrom(src => src.Email));
        }
        
    }
}
