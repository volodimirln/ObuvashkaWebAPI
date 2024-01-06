using System;
using System.Collections.Generic;

namespace ObuvashkaWebAPI.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Sum { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime Regdate { get; set; }
}
