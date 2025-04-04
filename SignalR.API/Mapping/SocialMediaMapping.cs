﻿using AutoMapper;
using SignalR.DataTransferObjectsLayer.SocialMediaDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Mapping
{
	public class SocialMediaMapping : Profile
	{
        public SocialMediaMapping()
        {
            CreateMap<SocialMedia, ResultSocialMediaDto>().ReverseMap();
			CreateMap<SocialMedia, CreateSocialMediaDto>().ReverseMap();
			CreateMap<SocialMedia, UpdateSocialMediaDto>().ReverseMap();
			CreateMap<SocialMedia, GetSocialMediaDto>().ReverseMap();
		}
    }
}
