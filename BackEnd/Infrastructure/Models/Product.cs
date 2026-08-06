namespace Infrastructure.Models;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Category category{get;set;}
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public uint Inventory { get; set; }=0;
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}