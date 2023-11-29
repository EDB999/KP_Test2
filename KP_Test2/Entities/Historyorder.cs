using System;
using System.Collections.Generic;
using KP_Test2.Entities;

namespace KP_Test2.Entities;


public partial class Historyorder
{
    public int Idorder { get; set; }

    public int Idpassenger { get; set; }

    public int Iddriver { get; set; }

    public int Price { get; set; }

    public string Routestart { get; set; } = null!;

    public string Routeend { get; set; } = null!;

    public DateTime Timestart { get; set; }

    public DateTime Timeend { get; set; }

    public virtual Driver IddriverNavigation { get; set; } = null!;

    public virtual Passenger IdpassengerNavigation { get; set; } = null!;
}
