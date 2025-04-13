using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.BookingDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IBookingService _bookingService;

		public BookingController(IBookingService bookingService)
		{
			_bookingService = bookingService;
		}

		[HttpGet]
		public IActionResult BookingList()
		{
			var values = _bookingService.TGetListAll();
			return Ok(values);
		}

		[HttpPost]
		public IActionResult CreateBooking(CreateBookingDto createBookingDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			Booking booking = new Booking()
			{
				Name = createBookingDto.Name,
				Phone = createBookingDto.Phone,
				Mail = createBookingDto.Email,
				PersonCount = createBookingDto.PersonCount.ToString(),
				Date = createBookingDto.Date
			};

			_bookingService.TAdd(booking);
			return Ok("Rezervasyon başarılı bir şekilde eklendi.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteBooking(int id)
		{
			var value = _bookingService.TGetById(id);
			_bookingService.TDelete(value);
			return Ok("Rezervasyon başarılı bir şekilde silindi.");
		}

		[HttpPut("{id}")]
		public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
		{
			Booking booking = new Booking()
			{
				BookingId = updateBookingDto.BookingId,
				Name = updateBookingDto.Name,
				Phone = updateBookingDto.Phone,
				Mail = updateBookingDto.Email,
				PersonCount = updateBookingDto.PersonCount.ToString(),
				Date = updateBookingDto.Date
			};

			_bookingService.TUpdate(booking);
			return Ok("Rezervasyon başarılı bir şekilde güncellendi.");
		}

		[HttpGet("{id}")]
		public IActionResult GetBooking(int id)
		{
			var value = _bookingService.TGetById(id);
			return Ok(value);
		}
	}
}
