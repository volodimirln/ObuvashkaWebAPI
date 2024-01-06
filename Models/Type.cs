using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Type
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? TypeObject { get; set; }

    public int? OzCategoryId { get; set; }

    public int? OzTyoeId8229 { get; set; }

    public virtual ICollection<Accessory> Accessories { get; } = new List<Accessory>();

    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
