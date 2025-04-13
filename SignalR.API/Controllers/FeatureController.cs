using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.FeatureDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeatureController : ControllerBase
	{
		private readonly IFeatureService _featureService;
		private readonly IMapper _mapper;

		public FeatureController(IFeatureService featureService, IMapper mapper)
		{
			_featureService = featureService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult FeatureList()
		{
			var value = _mapper.Map<List<ResultFeatureDto>>(_featureService.TGetListAll());
			return Ok(value);
		}

		[HttpPost]
		public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_featureService.TAdd(new Feature()
			{
				Title1 = createFeatureDto.Title1,
				Title2 = createFeatureDto.Title2,
				Title3 = createFeatureDto.Title3,
				Description1 = createFeatureDto.Description1,
				Description2 = createFeatureDto.Description2,
				Description3 = createFeatureDto.Description3,
			});

			return Ok("Öne çıkarılan başarılı bir şekilde eklendi.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteFeature(int id)
		{
			var value = _featureService.TGetById(id);
			if (value == null) return NotFound();

			_featureService.TDelete(value);
			return Ok("Öne çıkarılan başarılı bir şekilde silindi.");
		}

		[HttpGet("{id}")]
		public IActionResult GetFeature(int id)
		{
			var value = _featureService.TGetById(id);
			if (value == null) return NotFound();

			var result = _mapper.Map<ResultFeatureDto>(value);
			return Ok(value);
		}

		[HttpPut]
		public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
		{
			var value = _featureService.TGetById(updateFeatureDto.FeatureId);

			if (value == null)
			{
				return NotFound("Öne çıkarılan bulunamadı.");
			}

			value.FeatureId = updateFeatureDto.FeatureId;
			value.Title1 = updateFeatureDto.Title1;
			value.Title2 = updateFeatureDto.Title2;
			value.Title3 = updateFeatureDto.Title3;
			value.Description1 = updateFeatureDto.Description1;
			value.Description2 = updateFeatureDto.Description2;
			value.Description3 = updateFeatureDto.Description3;

			_featureService.TUpdate(value);
			return Ok("Öne çıkarılan başarılı bir şekilde güncellendi.");
		}
	}
}
