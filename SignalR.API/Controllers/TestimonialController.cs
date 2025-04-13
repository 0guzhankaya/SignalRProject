using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.TestimonialDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialController : ControllerBase
	{
		private readonly ITestimonialService _testimonialService;
		private readonly IMapper _mapper;

		public TestimonialController(ITestimonialService testimonialService, IMapper mapper)
		{
			_testimonialService = testimonialService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult TestimonialList()
		{
			var value = _mapper.Map<List<ResultTestimonialDto>>(_testimonialService.TGetListAll());
			return Ok(value);
		}

		[HttpPost]
		public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonialDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_testimonialService.TAdd(new Testimonial()
			{
				Name = createTestimonialDto.Name,
				Title = createTestimonialDto.Title,
				Comment = createTestimonialDto.Comment,
				ImageUrl = createTestimonialDto.ImageUrl,
				Status = true,
			});

			return Ok("Müşteri yorum bilgisi başarılı bir şekilde eklendi.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteTestimonial(int id)
		{
			var value = _testimonialService.TGetById(id);
			_testimonialService.TDelete(value);
			return Ok("Müşteri yorum bilgisi başarılı bir şekilde silindi.");
		}

		[HttpGet("{id}")]
		public IActionResult GetTestimonial(int id)
		{
			var value = _testimonialService.TGetById(id);

			if (value == null) return NotFound();

			var result = _mapper.Map<ResultTestimonialDto>(value);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
		{
			var value = _testimonialService.TGetById(updateTestimonialDto.TestimonialId);

			if (value == null)
			{
				return NotFound();
			}

			value.TestimonialId = updateTestimonialDto.TestimonialId;
			value.Name = updateTestimonialDto.Name;
			value.Title = updateTestimonialDto.Title;
			value.Comment = updateTestimonialDto.Comment;
			value.ImageUrl = updateTestimonialDto.ImageUrl;
			value.Status = updateTestimonialDto.Status;

			_testimonialService.TUpdate(value);
			return Ok("Müşteri yorum bilgisi başarılı bir şekilde güncellendi.");
		}
	}
}
