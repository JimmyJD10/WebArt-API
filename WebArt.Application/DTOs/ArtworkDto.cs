namespace WebArt.Application.DTOs;

using System;

public class ArtworkDto
{
	public Guid Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public decimal Price { get; set; }
	public Guid? OwnerId { get; set; }
}