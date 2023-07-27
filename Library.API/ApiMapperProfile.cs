using AutoMapper;
using Library.API.Models;
using Library.Domain.Entities;

namespace Library.API
{
    public class ApiMapperProfile:Profile
    {
        public ApiMapperProfile() 
        {
            CreateMap<BookModel, Book>().ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.Id==Guid.Empty?Guid.NewGuid():src.Id));

        }
    }
}
