using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Country 
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int OzCountryId4389 { get; set; }
    [JsonIgnore]
    public virtual ICollection<Brand> Brands { get; } = new List<Brand>();
}
