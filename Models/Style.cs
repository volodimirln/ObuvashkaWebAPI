using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Style
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int OzStyleId4501 { get; set; }

    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
