using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CatalogApi.Models;

public class Category
{
    public Category()
    {
        Products = new Collection<Product>();
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    //[Required]
    //[StringLength(300)]
    //public string? ImgUrl { get; set; }   
    [JsonIgnore]
    public ICollection<Product>? Products { get; set; }
}
