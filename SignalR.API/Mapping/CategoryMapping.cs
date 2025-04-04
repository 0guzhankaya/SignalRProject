using AutoMapper;
using SignalR.DataTransferObjectsLayer.CategoryDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Mapping
{
	public class CategoryMapping : Profile
	{
        public CategoryMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryDto>().ReverseMap();
        }
    }
}
