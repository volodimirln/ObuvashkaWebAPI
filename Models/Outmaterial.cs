using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

/// <summary>
/// Внутренний материал
/// </summary>
public partial class Outmaterial
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? OzOutmaterial4305 { get; set; }
    [JsonIgnore]
    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
