using AutoMapper;
using GifStorage.Data.Entities;
using GifStorage.Models;

namespace GifStorage.AutoMapper;

public class AppMappingProfile : Profile {
	public AppMappingProfile() {
		CreateMap<Gif, GifVm>()
			.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.GifTags.Select(gt => gt.Tag)));
		CreateMap<Gif, InnerGifVm>();
		CreateMap<CreateGifVm, Gif>();

		CreateMap<GifTag, GifTagVm>();
		CreateMap<CreateGifTagVm, GifTag>();
		CreateMap<DeleteGifTagVm, GifTag>();

		CreateMap<Tag, TagVm>()
			.ForMember(dest => dest.Gifs, opt => opt.MapFrom(src => src.GifTags.Select(gt => gt.Gif)));
		CreateMap<Tag, InnerTagVm>();
		CreateMap<CreateTagVm, Tag>();
	}
}
