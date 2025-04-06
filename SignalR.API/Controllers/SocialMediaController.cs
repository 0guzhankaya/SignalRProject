using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.SocialMediaDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SocialMediaController : ControllerBase
	{
		private readonly ISocialMediaService _socialMediaService;
		private readonly IMapper _mapper;

		public SocialMediaController(ISocialMediaService socialMediaService, IMapper mapper)
		{
			_socialMediaService = socialMediaService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult SocialMediaList()
		{
			var value = _mapper.Map<List<ResultSocialMediaDto>>(_socialMediaService.TGetListAll());
			return Ok(value);
		}

		[HttpPost]
		public IActionResult CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_socialMediaService.TAdd(new SocialMedia()
			{
				Title = createSocialMediaDto.Title,
				Icon = createSocialMediaDto.Icon,
				Url = createSocialMediaDto.Url,
			});

			return Ok("Sosyal medya bilgisi başarılı bir şekilde eklendi.");
		}

		[HttpDelete]
		public IActionResult DeleteSocialMedia(int id)
		{
			var value = _socialMediaService.TGetById(id);
			if (value == null)
			{
				return NotFound();
			}

			_socialMediaService.TDelete(value);
			return Ok("Sosyal medya başarılı bir şekilde silindi.");
		}

		[HttpGet("GetSocialMedia")]
		public IActionResult GetSocialMedia(int id)
		{
			var value = _socialMediaService.TGetById(id);
			if (value == null)
			{
				return NotFound("Sosyal medya bilgisi bulunamadı.");
			}

			var result = _mapper.Map<ResultSocialMediaDto>(value);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
		{
			var value = _socialMediaService.TGetById(updateSocialMediaDto.SocialMediaId);

			if (value == null)
			{
				return NotFound("Sosyal Medya bilgisi bulunamadı!");
			}

			value.SocialMediaId = updateSocialMediaDto.SocialMediaId;
			value.Title = updateSocialMediaDto.Title;
			value.Icon = updateSocialMediaDto.Icon;
			value.Url = updateSocialMediaDto.Url;

			_socialMediaService.TUpdate(value);
			return Ok("Sosyal medya bilgisi başarılı bir şekilde güncellendi.");
		}
	}
}
