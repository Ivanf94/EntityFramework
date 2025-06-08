namespace Racuni.Models
{
    public class InvoiceItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal InvoiceItemTotal()
        {
            return Price * Quantity;
        }
    }
}