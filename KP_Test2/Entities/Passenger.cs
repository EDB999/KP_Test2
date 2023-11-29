using System;
using System.Collections.Generic;

namespace KP_Test2.Entities;

public partial class Passenger
{
    public int Idpassenger { get; set; }

    public double? Rating { get; set; }

    public string? Description { get; set; }

    public string? Addresshome { get; set; }

    public int Iduser { get; set; }

    public virtual ICollection<Historyorder> Historyorders { get; set; } = new List<Historyorder>();

    public virtual Usertaxi IduserNavigation { get; set; } = null!;
}
