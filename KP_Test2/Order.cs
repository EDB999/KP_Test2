using System;
using System.Collections.Generic;

namespace KP_Test2;

public partial class Order
{
    public int IdOrders { get; set; }

    public string? Address { get; set; }

    public string? Time { get; set; }

    public int? Price { get; set; }

    public DateOnly? Date { get; set; }
}
