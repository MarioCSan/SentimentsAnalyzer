using PrompSentiments.Domain.Models;

namespace PrompSentiments.Aplication.Interfaces
{
    public interface ISentimentAnalysisService
    {
        SentimentPrediction AnalyzeSentiment(string text);
    }

    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        public SentimentPrediction AnalyzeSentiment(string text)
        {
            // Aquí va la lógica para predecir el sentimiento.
            // Ejemplo: Llamada a un modelo de machine learning o API externa.

            var sentiment = SentimentType.Neutral;  // Predicción de ejemplo
            var probability = 0.75f;
            var score = 0.85f;

            if (text.Contains("happy"))
            {
                sentiment = SentimentType.Happy;
            }
            else if (text.Contains("angry"))
            {
                sentiment = SentimentType.Angry;
            }

            return new SentimentPrediction
            {
                SentimentText = text,
                Sentiment = sentiment,
                PredictedLabel = sentiment,
                Probability = probability,
                Score = score
            };
        }
    }

}
