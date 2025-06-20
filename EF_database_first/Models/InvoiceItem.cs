using System;
using System.Collections.Generic;

namespace EF_database_first.Models;

public partial class InvoiceItem
{
    public int Id { get; set; }

    public int InvoiceNumber { get; set; }

    public string? Title { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Invoice InvoiceNumberNavigation { get; set; } = null!;
}
