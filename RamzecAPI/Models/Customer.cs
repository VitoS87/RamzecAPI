using System;
using System.Collections.Generic;

namespace RamzecAPI.Models;

public partial class Customer
{
    public string CustomerName { get; set; } = null!;

    public string CustomerAdress { get; set; } = null!;

    public long CustomerPhone { get; set; }

    public int CustomerCode { get; set; }

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();
}
