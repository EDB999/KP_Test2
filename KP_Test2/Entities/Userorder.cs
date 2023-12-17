using System;
using System.Collections.Generic;

namespace KP_Test2.Entities;

public partial class Userorder
{
    public int Iduserorder { get; set; }

    public int Iduser { get; set; }

    public int Iddriver { get; set; }

    public string Route { get; set; }

    public double Paysize { get; set; }

    public virtual Driver IddriverNavigation { get; set; } = null!;

    public virtual Passenger IduserNavigation { get; set; } = null!;
}
