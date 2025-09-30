﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComApp.Models
{
    [Table("OrderDetail")]
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
