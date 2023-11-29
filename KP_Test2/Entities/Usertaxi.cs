using System;
using System.Collections.Generic;
using KP_Test2.Entities;

namespace KP_Test2.Entities;
public partial class Usertaxi
{
    public int Iduser { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? Contact { get; set; }

    public long? Card { get; set; }

    public DateOnly Dateregistration { get; set; }

    public string? Roletype { get; set; }

    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
