namespace TaskThinkland.Api.DTOs.ProductDTOs;

public class UpdateProductDto
{
    public IFormFile? ImageFile { get; set; }
    public string? Title { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
}