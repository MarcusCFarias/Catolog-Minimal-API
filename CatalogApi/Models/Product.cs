using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
    [Required]
    [StringLength(300)]
    public string? Description { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
    public string? ImgUrl { get; set; }
    public int Stock { get; set; }
    public DateTime PurchaseDate { get; set; }
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
}
