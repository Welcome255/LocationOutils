using System;
using System.Collections.Generic;

namespace Backend.Entites;

public partial class Equipement
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
