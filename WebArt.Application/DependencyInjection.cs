namespace WebArt.Application;

using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using System.Reflection;

public static class DependencyInjection
{
		public static IServiceCollection AddApplication(this IServiceCollection services)
	{
	// Register AutoMapper profile from this assembly
	services.AddAutoMapper(cfg => cfg.AddProfile<Mappings.ApplicationProfile>());
	// Register MediatR handlers from this assembly
	services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

		// Services
		services.AddScoped<Services.UserService>();
		services.AddScoped<Services.ArtworkService>();

		return services;
	}
}