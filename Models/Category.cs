using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComApp.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)] 
        public string? CategoryName { get; set; }

        public List<Article> Articles { get; set; }
    }
}