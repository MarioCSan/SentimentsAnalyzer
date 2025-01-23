namespace PrompSentiments.Domain.Models
{
    public class SentimentPrediction : SentimentData
    {
        public float Probability { get; set; }
        public float Score { get; set; }
        public SentimentType Sentiment { get; set; }
        public SentimentType PredictedLabel { get; set; }
    }
}
