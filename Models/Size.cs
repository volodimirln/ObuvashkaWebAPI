using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Size
{
    public int Id { get; set; }

    public int ShoesId { get; set; }

    public int Size1 { get; set; }

    public int? Num { get; set; }

    public int? OzSizeId4298 { get; set; }
    [JsonIgnore]
    public virtual Shoe Shoes { get; set; } = null!;
}
