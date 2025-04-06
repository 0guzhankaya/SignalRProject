using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.CategoryDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService, IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult CategoryList()
		{
			var value = _mapper.Map<List<ResultCategoryDto>>(_categoryService.TGetListAll());
			return Ok(value);
		}

		[HttpPost]
		public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_categoryService.TAdd(new Category()
			{
				CategoryName = createCategoryDto.CategoryName,
				CategoryStatus = true
			});

			return Ok("Kategori başarılı bir şekilde eklendi.");
		}

		[HttpDelete]
		public IActionResult DeleteCategory(int id)
		{
			var value = _categoryService.TGetById(id);
			_categoryService.TDelete(value);
			return Ok("Kategori başarılı bir şekilde silindi.");
		}

		[HttpGet("GetCategory")]
		public IActionResult GetCategory(int id)
		{
			var value = _categoryService.TGetById(id);

			if (value == null)
			{
				return NotFound("Kategori bulunamadı.");
			}

			var result = _mapper.Map<ResultCategoryDto>(value);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			var value = _categoryService.TGetById(updateCategoryDto.CategoryId);

			if (value == null)
			{
				return NotFound("Kategori bulunamadı.");
			}

			value.CategoryName = updateCategoryDto.CategoryName;
			value.CategoryStatus = updateCategoryDto.Status;

			_categoryService.TUpdate(value);
			return Ok("Kategori başarılı bir şekilde güncellendi.");
		}
	}
}
