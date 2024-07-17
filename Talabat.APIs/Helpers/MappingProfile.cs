using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using UserAddress = Talabat.Core.Entities.Identity.Address;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.Brand, P => P.MapFrom(S => S.Brand.Name))
                .ForMember(D => D.Category, P => P.MapFrom(S => S.Category.Name))
                .ForMember(D => D.PictureUrl, P => P.MapFrom<ProductPictureUrlResolver>());

            //CreateMap<RegisterDto, AppUser>()
            //    .ForMember(U => U.UserName, U => U.MapFrom(D => D.Email.Split('@', '@')[0]));

            //CreateMap<IdentityResult, UserDto>();

            CreateMap<UserAddress, AddressDto>().ReverseMap();
            CreateMap<BuyerAddress, AddressDto>().ReverseMap();
        }
    }
}
