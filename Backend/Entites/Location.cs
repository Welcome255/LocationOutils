using System;
using System.Collections.Generic;

namespace Backend.Entites;

public partial class Location
{
    public int Id { get; set; }

    public DateOnly DateDe { get; set; }

    public DateOnly DateA { get; set; }

    public bool Active { get; set; }

    public int EquipementId { get; set; }

    public virtual Equipement Equipement { get; set; } = null!;
}
