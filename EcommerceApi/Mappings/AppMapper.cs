using AutoMapper;
using EcommerceApi.DTOs;
using EcommerceApi.Models;
using System.Security.Cryptography;
namespace EcommerceApi.Mappings
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<WishList, WishListDTO>().ReverseMap();
        }
    }
}
