using System;
using System.Collections.Generic;

namespace TechMegStore.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? Name { get; set; }

    public int? IdCategory { get; set; }

    public int? Stock { get; set; }

    public decimal? Price { get; set; }

    public bool? Active { get; set; }

    public DateTime? DateRegistration { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
