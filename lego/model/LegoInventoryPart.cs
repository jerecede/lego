using System;
using System.Collections.Generic;

namespace lego.model;

public partial class LegoInventoryPart
{
    public int InventoryId { get; set; }

    public string PartNum { get; set; } = null!;

    public int ColorId { get; set; }

    public int Quantity { get; set; }

    public bool IsSpare { get; set; }

    public virtual LegoColor Color { get; set; } = null!;

    public virtual LegoInventory Inventory { get; set; } = null!;

    public virtual LegoPart PartNumNavigation { get; set; } = null!;
}
