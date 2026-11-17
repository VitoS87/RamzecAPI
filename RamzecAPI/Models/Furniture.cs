using System;
using System.Collections.Generic;

namespace RamzecAPI.Models;

public partial class Furniture
{
    public string FurnitureCharacteristics { get; set; } = null!;

    public decimal FurnitureCost { get; set; }

    public string FurnitureName { get; set; } = null!;

    public string FurnitureModel { get; set; } = null!;

    public virtual ICollection<Sale> SaleFurnitureModelNavigations { get; set; } = new List<Sale>();

    public virtual ICollection<Sale> SaleFurnitureNameNavigations { get; set; } = new List<Sale>();
}
