using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pustok.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Desc { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        [Column(TypeName = "money")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "money")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        [Required]
        public bool StockStatus { get; set; }
        public bool IsBestSeller{ get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Genre Genre { get; set; }
        public Author Author { get; set; }

        public ICollection<BookTag> BookTags { get; set;}
        public ICollection<BookImage> BookImages { get; set;}
    }
}
