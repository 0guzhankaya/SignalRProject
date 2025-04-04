using AutoMapper;
using SignalR.DataTransferObjectsLayer.DiscountDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Mapping
{
	public class DiscountMapping : Profile
	{
        public DiscountMapping()
        {
            CreateMap<Discount, ResultDiscountDto>().ReverseMap();
            CreateMap<Discount, CreateDiscountDto>().ReverseMap();
			CreateMap<Discount, UpdateDiscountDto>().ReverseMap();
			CreateMap<Discount, GetDiscountDto>().ReverseMap();
		}
    }
}
