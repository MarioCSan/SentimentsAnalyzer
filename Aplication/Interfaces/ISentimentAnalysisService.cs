using PrompSentiments.Domain.Models;

namespace PrompSentiments.Application.Interfaces
{
    public interface ISentimentAnalysisService
    {
        SentimentPrediction AnalyzeSentiment(string text);
        string GenerateResponse(SentimentType sentiment);
    }
}
