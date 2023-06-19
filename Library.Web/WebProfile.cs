using AutoMapper;
using Library.Domain.Entities;
using Library.Web.Areas.Admin.Models;

namespace Library.Web
{
    public class WebProfile:Profile
    {
        public WebProfile() {
            CreateMap<BookCreateModel, Book>()
                .ForMember(dest => dest.Id, opt=>opt.MapFrom(x=>Guid.NewGuid()));

        }
    }
}
