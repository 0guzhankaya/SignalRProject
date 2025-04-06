using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult ProductList()
		{
			var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
			return Ok(value);
		}

		[HttpPost]
		public IActionResult CreateProduct(CreateProductDto createProductDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_productService.TAdd(new Product()
			{
				ProductName = createProductDto.ProductName,
				Price = createProductDto.Price,
				ProductDescription = createProductDto.Description,
				ImageUrl = createProductDto.ImageUrl,
				ProductStatus = true,
			});

			return Ok("Ürün başarılı bir şekilde eklendi.");
		}

		[HttpDelete]
		public IActionResult DeleteProduct(int id)
		{
			var value = _productService.TGetById(id);

			if (value == null) return NotFound();

			var result = _mapper.Map<ResultProductDto>(value);
			return Ok(result);
		}

		[HttpGet("GetProduct")]
		public IActionResult GetCategory(int id)
		{
			var value = _productService.TGetById(id);

			if (value == null)
			{
				return NotFound("Ürün bulunamadı.");
			}

			var result = _mapper.Map<ResultProductDto>(value);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
		{
			var value = _productService.TGetById(updateProductDto.ProductId);

			if (value == null)
			{
				return NotFound("Ürün bulunamadı!");
			}

			value.ProductId = updateProductDto.ProductId;
			value.ProductName = updateProductDto.ProductName;
			value.Price = updateProductDto.Price;
			value.ProductDescription = updateProductDto.Description;
			value.ImageUrl = updateProductDto.ImageUrl;
			value.ProductStatus = updateProductDto.ProductStatus;

			_productService.TUpdate(value);
			return Ok("Ürün başarılı bir şekilde güncellendi.");
		}
	}
}
