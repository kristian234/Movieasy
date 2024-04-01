namespace Movieasy.Application.Reviews.GetReview
{
    public sealed class ReviewResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string ReviewerName { get; set; } = string.Empty;
        public int Rating { get; set; } 
        public string CreatedOnDate { get; set; } = string.Empty;
    }
}
