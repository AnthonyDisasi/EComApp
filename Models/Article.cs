using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComApp.Models
{
    [Table("Article")]
    public class Article
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string? ArticleName { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<CartDetail> CartDetails { get; set; }
    }
}
