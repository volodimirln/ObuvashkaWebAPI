using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Shoe
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
    [JsonIgnore]
    public virtual Brand? BrandNavigation { get; set; }
    [JsonIgnore]
    public virtual Color? Color { get; set; }
    [JsonIgnore]
    public virtual Gender Gender { get; set; } = null!;
    [JsonIgnore]
    public virtual InsoleMaterial? InsoleMaterialNavigation { get; set; }
    [JsonIgnore]
    public virtual Material? MaterialsNavigation { get; set; }
    [JsonIgnore]
    public virtual Outmaterial? OutmaterialNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<PictureToProduct> PictureToProducts { get; } = new List<PictureToProduct>();
    [JsonIgnore]
    public virtual Season? Season { get; set; }
    [JsonIgnore]
    public virtual ICollection<ShoesOzonArchive> ShoesOzonArchives { get; } = new List<ShoesOzonArchive>();
    [JsonIgnore]
    public virtual ICollection<Size> Sizes { get; } = new List<Size>();
    [JsonIgnore]
    public virtual Style? Style { get; set; }
    [JsonIgnore]
    public virtual Tnved? Tnved { get; set; }
    [JsonIgnore]
    public virtual Type Type { get; set; } = null!;
}
