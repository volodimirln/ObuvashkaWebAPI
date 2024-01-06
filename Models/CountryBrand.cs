using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class CountryBrand 
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string OzCountryId9248 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Brand> Brands { get; } = new List<Brand>();
}
