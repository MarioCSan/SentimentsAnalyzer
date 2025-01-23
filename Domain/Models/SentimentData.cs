namespace PrompSentiments.Domain.Models
{
    public enum SentimentType
    {
        Happy = 0,
        Angry = 1,
        Neutral = 2
    }

    public class SentimentData
    {
        public string SentimentText { get; set; }
        public float SentimentLabel { get; set; } 
    }
}
