namespace PrompSentiments.Domain.Models
{
    public class SentimentData
    {
        public string SentimentText { get; set; }
        public bool Sentiment { get; set; }
    }

    public class SentimentPrediction : SentimentData
    {
        public float Probability { get; set; }
        public float Score { get; set; }
        public bool PredictedLabel { get; set; }
    }

}
