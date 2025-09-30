using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComApp.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int ArticleId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Article Article { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
