using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComApp.Models
{
    public class OrderStatus
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string? StatusName { get; set; }
    }
}
