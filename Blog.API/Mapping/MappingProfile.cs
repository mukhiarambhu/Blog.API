using AutoMapper;
using Blog.API.Models.Domain;
using Blog.API.Models.Dto;

namespace Blog.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, categoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();
            CreateMap<BlogPost, BlogPostDto>().ReverseMap();
            CreateMap<AddBlogRequestDto, BlogPost>().ReverseMap();
            CreateMap<UpdateBlogRequest, BlogPost>().ReverseMap();
        }
    }
}
