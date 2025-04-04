using AutoMapper;
using SignalR.DataTransferObjectsLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Mapping
{
	public class AboutMapping : Profile
	{
		public AboutMapping()
		{
			CreateMap<About, ResultAboutDto>().ReverseMap();
			CreateMap<About, CreateAboutDto>().ReverseMap();
			CreateMap<About, UpdateAboutDto>().ReverseMap();
			CreateMap<About, GetAboutDto>().ReverseMap();
		}
	}
}
