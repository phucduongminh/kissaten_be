namespace ItssProject.Models
{
    public class ReviewReaction
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public bool like { get; set; }
        public bool DisLike { get; set; }
        public DateTime LikedAt { get; set; }
        public DateTime DisLikedAt { get; set; }
    }
}
