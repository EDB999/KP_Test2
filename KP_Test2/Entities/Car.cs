using System;
using System.Collections.Generic;

namespace KP_Test2.Entities;

public partial class Car
{
    public int Idcar { get; set; }

    public string Model { get; set; } = null!;

    public string Numbers { get; set; } = null!;

    public bool? Isautopark { get; set; }

    public bool IsFree { get; set; } = default;

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
