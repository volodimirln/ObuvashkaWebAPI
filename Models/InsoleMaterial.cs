using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

/// <summary>
/// Материал подошвы
/// </summary>
public partial class InsoleMaterial
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? OzInsoleId4516 { get; set; }
    [JsonIgnore]
    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
