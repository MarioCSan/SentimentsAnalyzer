using Microsoft.ML.Data;

namespace PrompSentiments.Domain.Models
{
    public enum SentimentType
    {
        Angry = 0,
        Happy = 1
    }

    public class SentimentData
    {
        [LoadColumn(0)]
        public uint Sentiment { get; set; }

        [LoadColumn(1)]
        public string SentimentText { get; set; }

        [LoadColumn(2)]
        public bool LoggedIn { get; set; }
    }
}
