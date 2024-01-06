using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Season
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? OzSeasonId4495 { get; set; }
    [JsonIgnore]
    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
