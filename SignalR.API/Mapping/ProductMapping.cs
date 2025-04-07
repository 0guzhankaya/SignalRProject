﻿using AutoMapper;
using SignalR.DataTransferObjectsLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Mapping
{
	public class ProductMapping : Profile
	{
		public ProductMapping()
		{
			CreateMap<Product, ResultProductDto>().ReverseMap();
			CreateMap<Product, CreateProductDto>().ReverseMap();
			CreateMap<Product, UpdateProductDto>().ReverseMap();
			CreateMap<Product, GetProductDto>().ReverseMap();
			CreateMap<Product, ResultProductWithCategory>().ReverseMap();
		}
	}
}
