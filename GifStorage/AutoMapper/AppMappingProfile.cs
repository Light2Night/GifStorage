using AutoMapper;
using GifStorage.Data.Entities;
using GifStorage.Models;

namespace GifStorage.AutoMapper;

public class AppMappingProfile : Profile {
	public AppMappingProfile() {
		CreateMap<Gif, GifVm>();
		CreateMap<AddGifVm, Gif>();
	}
}
