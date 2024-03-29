﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Bag
{
    public int Id { get; set; }

    public string? VendorCode { get; set; }

    public int MaterialOutside { get; set; }

    public int MaterialInside { get; set; }

    public string Color { get; set; } = null!;

    public int Price { get; set; }

    public int BrandId { get; set; }

    public int GenderId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public float? Discount { get; set; }

    public int? Popularity { get; set; }

    public int? Num { get; set; }
    [JsonIgnore]

    public virtual Brand Brand { get; set; } = null!;
    [JsonIgnore]
    public virtual Gender Gender { get; set; } = null!;
    [JsonIgnore]
    public virtual Material MaterialInsideNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual Material MaterialOutsideNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<PictureToBag> PictureToBags { get; } = new List<PictureToBag>();
}
