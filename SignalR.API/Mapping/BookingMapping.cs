using AutoMapper;
using SignalR.DataTransferObjectsLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Mapping
{
	public class BookingMapping : Profile
	{
        public BookingMapping()
        {
            CreateMap<Booking, ResultBookingDto>().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().ReverseMap();
            CreateMap<Booking, UpdateBookingDto>().ReverseMap();
			CreateMap<Booking, GetBookingDto>().ReverseMap();
		}
    }
}
