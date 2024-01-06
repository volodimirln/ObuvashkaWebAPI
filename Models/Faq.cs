using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Faq
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? ImglLink { get; set; }
}
