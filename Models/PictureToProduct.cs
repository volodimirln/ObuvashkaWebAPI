using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class PictureToProduct
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string PhotoPath { get; set; } = null!;
    public bool Status { get; set; }
    public string vendorCode { get { return Product.VendorCode; } }
    [JsonIgnore]
    public virtual Shoe Product { get; set; } = null!;
}
