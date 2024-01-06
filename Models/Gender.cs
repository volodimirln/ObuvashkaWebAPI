using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Gender
{
    public int Id { get; set; }

    public string Gender1 { get; set; } = null!;

    public string? OzGenderId9163 { get; set; }

    [JsonIgnore]
    public virtual ICollection<Bag> Bags { get; } = new List<Bag>();
    [JsonIgnore]
    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
