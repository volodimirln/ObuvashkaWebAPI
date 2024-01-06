using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Shoesview
{
    public int Id { get; set; }

    public string VendorCode { get; set; } = null!;

    public string? Title { get; set; }

    public string Description { get; set; } = null!;

    public int Price { get; set; }

    public bool Markdown { get; set; }

    public int GenderId { get; set; }

    public int? SeasonId { get; set; }

    public int TypeId { get; set; }

    public int? Brand { get; set; }

    public int? Materials { get; set; }

    public string? Outmaterial { get; set; }

    public int? OutmaterialId { get; set; }

    public float? Discount { get; set; }

    public int? Popularity { get; set; }

    public string? InsoleMaterial { get; set; }

    public int? InsoleMaterialId { get; set; }

    public int? ColorId { get; set; }

    public int? StyleId { get; set; }

    public string? LwhwPackage { get; set; }

    public int? TnvedId { get; set; }

    public int? ImportOzon { get; set; }

    public int? PermanentlyOzon { get; set; }

    public DateTime? TimeToAdd { get; set; }
}
