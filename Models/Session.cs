using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ObuvashkaWebAPI.Models;

public partial class Session
{
    public int Id { get; set; }

    public int AdminId { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? TimeEnter { get; set; }
    [JsonIgnore]
    public virtual Administrarion Admin { get; set; } = null!;
}
