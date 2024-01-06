using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class PictureToBag
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public string PhotoPath { get; set; } = null!;

    public bool Status { get; set; }
    [NotMapped]
    public string vendorCode { get { return Product.VendorCode; } }
    [JsonIgnore]
    public virtual Bag Product { get; set; } = null!;
}
