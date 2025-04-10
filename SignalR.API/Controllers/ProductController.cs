using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
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

		[HttpGet("GetProductListWithCategories")]
		public IActionResult GetProductListWithCategories()
		{
			var context = new SignalRContext();

			var values = context.Products.Include(x => x.Categories).Select(y => new ResultProductWithCategory
			{
				Description = y.ProductDescription,
				ImageUrl = y.ImageUrl,
				Price = y.Price,
				ProductId = y.ProductId,
				ProductName = y.ProductName,
				ProductStatus = y.ProductStatus,
				CategoryName = y.Categories.CategoryName // Assuming you have a navigation property for Categories
			});

			return Ok(values.ToList());
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
				CategoryId = createProductDto.CategoryId,
			});

			return Ok("Ürün başarılı bir şekilde eklendi.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteProduct(int id)
		{
			var value = _productService.TGetById(id);

			if (value == null) return NotFound();

			var result = _mapper.Map<ResultProductDto>(value);
			return Ok(result);
		}

		[HttpGet("{id}")]
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
			value.CategoryId = updateProductDto.CategoryId;

			_productService.TUpdate(value);
			return Ok("Ürün başarılı bir şekilde güncellendi.");
		}
	}
}
