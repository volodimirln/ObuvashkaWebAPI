using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class ShoesOzonArchive
{
    public int Id { get; set; }

    public int ShoesId { get; set; }

    public string Attributes { get; set; } = null!;

    public virtual Shoe Shoes { get; set; } = null!;
}
