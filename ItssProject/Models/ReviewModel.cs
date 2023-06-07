using System.ComponentModel.DataAnnotations;

namespace ItssProject.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CoffeeId { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewAt { get; set; }
        public DateTime EditAt { get; set; }
    }
}
