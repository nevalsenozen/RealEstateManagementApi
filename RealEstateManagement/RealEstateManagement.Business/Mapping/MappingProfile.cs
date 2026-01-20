using AutoMapper;
using RealEstateManagement.Entity.Concrete; 
using RealEstateManagement.Business.Dto;

namespace RealEstateManagement.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<PropertyCreateDto, Property>();
            CreateMap<PropertyUpdateDto, Property>();

            
            CreateMap<InquiryCreateDto, Inquiry>();

            
            CreateMap<RegisterDto, AppUser>();
            CreateMap<LoginDto, AppUser>();

            
            CreateMap<RefreshTokenCreateDto, RefreshToken>();
            CreateMap<RefreshTokenDto, RefreshToken>();
        }
    }
}
