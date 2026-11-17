using System;
using System.Collections.Generic;

namespace RamzecAPI.Models;

public partial class Sale
{
    public long DealNumber { get; set; }

    public string FurnitureName { get; set; } = null!;

    public string FurnitureModel { get; set; } = null!;

    public int SalesAmount { get; set; }

    public int Saleid { get; set; }

    public virtual Deal DealNumberNavigation { get; set; } = null!;

    public virtual Furniture FurnitureModelNavigation { get; set; } = null!;

    public virtual Furniture FurnitureNameNavigation { get; set; } = null!;
}
