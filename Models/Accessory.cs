using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Accessory
{
    public int Id { get; set; }

    public string? VendorCode { get; set; }

    public string? Title { get; set; }

    public int? Price { get; set; }

    public int? TypeId { get; set; }

    public string? Description { get; set; }

    public int? SizeHead { get; set; }

    public string? Color { get; set; }

    public float? Discount { get; set; }

    public int? Popularity { get; set; }

    public int? Num { get; set; }
    [JsonIgnore]
    public virtual ICollection<PictureToAccessory> PictureToAccessories { get; } = new List<PictureToAccessory>();
    [JsonIgnore]
    public virtual Type? Type { get; set; }
}
