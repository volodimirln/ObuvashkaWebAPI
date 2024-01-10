using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Tnved
{
    public int Id { get; set; }

    public string Num { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string OzTnvedid22232 { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
