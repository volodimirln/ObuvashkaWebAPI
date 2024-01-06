﻿using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

/// <summary>
/// Внутренний материал
/// </summary>
public partial class Outmaterial
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? OzOutmaterial4305 { get; set; }

    public virtual ICollection<Shoe> Shoes { get; } = new List<Shoe>();
}
