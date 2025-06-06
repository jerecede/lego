using System;
using System.Collections.Generic;

namespace lego.model;

public partial class LegoInventorySet
{
    public int InventoryId { get; set; }

    public string SetNum { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual LegoInventory Inventory { get; set; } = null!;

    public virtual LegoSet SetNumNavigation { get; set; } = null!;
}
