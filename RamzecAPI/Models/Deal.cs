using System;
using System.Collections.Generic;

namespace RamzecAPI.Models;

public partial class Deal
{
    public string DealDateMade { get; set; } = null!;

    public string DealDateDone { get; set; } = null!;

    public long DealNumber { get; set; }

    public int CustomerCode { get; set; }

    public virtual Customer CustomerCodeNavigation { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
