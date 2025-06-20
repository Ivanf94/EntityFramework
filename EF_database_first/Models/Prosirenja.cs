using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EF_database_first.Models
{
    public class Prosirenja
    {
    }

    public partial class InvoiceItem
    {
        public decimal? ItemTotal()
        {
            return Quantity * Price;
        }
    }

    public partial class Invoice()
    {
        public decimal? Total()
        {
            decimal? total = 0;
            foreach(var item in this.InvoiceItems)
            {
                total += item.ItemTotal();
            }
            return total;
        }
    }
}
