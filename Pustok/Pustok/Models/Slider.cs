using System.ComponentModel.DataAnnotations;

namespace Pustok.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required] 
        [MaxLength(35)]
        public string Title1 { get; set; }
        [MaxLength(35)]
        public string Title2 { get; set; }
        [MaxLength(150)]
        public string Desc { get; set; }
        [Required]
        [MaxLength(100)]
        public string Image { get; set; }
        [MaxLength(50)]
        public string BtnText { get; set; }
        [MaxLength(250)]
        public string BtnUrl { get; set; }
    }
}
