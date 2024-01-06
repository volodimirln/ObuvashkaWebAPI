using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Brand
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? OzBrandId31 { get; set; }

    public int? CountryManufId { get; set; }

    public int? CountryId { get; set; }

    public ulong? Doc { get; set; }
    [JsonIgnore]
    public virtual ICollection<Bag> Bags { get; } = new List<Bag>();
    [JsonIgnore]
    public virtual CountryBrand? Country { get; set; }
    [JsonIgnore]
    public virtual Country? CountryManuf { get; set; }
    [JsonIgnore]
    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
