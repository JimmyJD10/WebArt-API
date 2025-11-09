namespace WebArt.Application.Mappings;

using AutoMapper;
using WebArt.Domain.Entities;
using WebArt.Application.DTOs;

public class ApplicationProfile : Profile
{
	public ApplicationProfile()
	{
		CreateMap<User, UserDto>().ReverseMap();
		CreateMap<Artwork, ArtworkDto>().ReverseMap();
		// Add other entity <-> DTO mappings as needed
	}
}