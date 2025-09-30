using System.ComponentModel.DataAnnotations;

namespace EComApp.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public Article Article { get; set; }
        public Order Order { get; set; }
    }
}
