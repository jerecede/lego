using System;
using System.Collections.Generic;

namespace lego.model;

public partial class LegoInventory
{
    public int Id { get; set; }

    public int Version { get; set; }

    public string SetNum { get; set; } = null!;

    public virtual LegoSet SetNumNavigation { get; set; } = null!;
}
