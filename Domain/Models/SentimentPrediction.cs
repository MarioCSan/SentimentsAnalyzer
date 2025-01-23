namespace PrompSentiments.Domain.Models
{
    public class SentimentPrediction : SentimentData
    {
        public float Probability { get; set; }
        public float[] Score { get; set; }  // Cambio aquí
        public uint PredictedLabel { get; set; }
    }
}