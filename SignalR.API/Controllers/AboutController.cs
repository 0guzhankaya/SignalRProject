using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.AboutDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutController : ControllerBase
	{
		private readonly IAboutService _aboutService;

		public AboutController(IAboutService aboutService)
		{
			_aboutService = aboutService;
		}

		[HttpGet]
		public IActionResult AboutList()
		{
			var values = _aboutService.TGetListAll();
			return Ok(values);
		}

		[HttpPost]
		public IActionResult CreateAbout(CreateAboutDto createAboutDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			About about = new About()
			{
				Title = createAboutDto.Title,
				Description = createAboutDto.Description,
				ImageUrl = createAboutDto.ImageUrl
			};

			_aboutService.TAdd(about);
			return Ok("Hakkında kısmı başarılı bir şekilde eklendi.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteAbout(int id)
		{
			var value = _aboutService.TGetById(id);
			_aboutService.TDelete(value);
			return Ok("Hakkında kısmı başarılı bir şekilde silindi.");
		}

		[HttpPut]
		public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
		{
			if (ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			About about = new About()
			{
				AboutId = updateAboutDto.AboutId,
				Title = updateAboutDto.Title,
				Description = updateAboutDto.Description,
				ImageUrl = updateAboutDto.ImageUrl
			};

			_aboutService.TUpdate(about);
			return Ok("Hakkında kısmı başarılı bir şekilde güncellendi.");
		}

		[HttpGet("{id}")]
		public IActionResult GetAbout(int id)
		{
			var value = _aboutService.TGetById(id);
			return Ok(value);
		}
	}
}
