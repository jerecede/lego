using System;
using System.Collections.Generic;

namespace lego.model;

public partial class LegoTheme
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public virtual ICollection<LegoTheme> InverseParent { get; set; } = new List<LegoTheme>();

    public virtual ICollection<LegoSet> LegoSets { get; set; } = new List<LegoSet>();

    public virtual LegoTheme? Parent { get; set; }
}
