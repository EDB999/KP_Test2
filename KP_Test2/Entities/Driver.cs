﻿using System;
using System.Collections.Generic;

namespace KP_Test2.Entities;

public partial class Driver
{
    public int Iddriver { get; set; }

    public double? Rating { get; set; }

    public string? Description { get; set; }

    public bool Iswork { get; set; }

    public string Plane { get; set; } = null!;

    public int? Idcar { get; set; }

    public int License { get; set; }

    public string Lastplace { get; set; } = "Мира 1";

    public int Iduser { get; set; }

    public virtual ICollection<Historyorder> Historyorders { get; set; } = new List<Historyorder>();

    public virtual Car? IdcarNavigation { get; set; }

    public virtual Usertaxi IduserNavigation { get; set; } = null!;

    public virtual ICollection<Userorder> Userorders { get; set; } = new List<Userorder>();
}
