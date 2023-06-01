using System.ComponentModel.DataAnnotations;

namespace ItssProject.Models
{
    public class ReviewImage
    {
        [Key]
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string? Image { get; set; }
    }
}
