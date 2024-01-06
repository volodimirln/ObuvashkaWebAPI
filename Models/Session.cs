using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Session
{
    public int Id { get; set; }

    public int AdminId { get; set; }

    public string? IpAddress { get; set; }

    public DateTime? TimeEnter { get; set; }

    public virtual Administrarion Admin { get; set; } = null!;
}
