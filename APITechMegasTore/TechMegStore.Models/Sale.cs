using System;
using System.Collections.Generic;

namespace TechMegStore.Models;

public partial class Sale
{
    public int IdSale { get; set; }

    public string? DocumentNumber { get; set; }

    public string? PaymentType { get; set; }

    public decimal? SaleTotal { get; set; }

    public DateTime? DateRegistration { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
