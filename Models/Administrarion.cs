using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Administrarion
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Token { get; set; } = null!;

    public virtual ICollection<Session> Sessions { get; } = new List<Session>();
}
