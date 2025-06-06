using System;
using System.Collections.Generic;

namespace lego.model;

public partial class LegoSet
{
    public string SetNum { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? Year { get; set; }

    public int? ThemeId { get; set; }

    public int? NumParts { get; set; }

    public virtual ICollection<LegoInventory> LegoInventories { get; set; } = new List<LegoInventory>();

    public virtual LegoTheme? Theme { get; set; }
}
