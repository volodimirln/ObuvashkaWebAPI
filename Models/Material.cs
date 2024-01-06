﻿using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? OzMaterialId4496 { get; set; }

    public virtual ICollection<Bag> BagMaterialInsideNavigations { get; } = new List<Bag>();

    public virtual ICollection<Bag> BagMaterialOutsideNavigations { get; } = new List<Bag>();

    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
