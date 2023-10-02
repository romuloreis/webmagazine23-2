using WebMagazine.Models.Enums;

namespace WebMagazine.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public SaleStatus Status { get; set; }
        /* Define relação com vendedor */
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
    }
}
