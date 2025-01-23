namespace PrompSentiments.Application.Services
{
    using PrompSentiments.Domain.Models;
    using Microsoft.ML;
    using Microsoft.ML.Data;

    public class SentimentPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentPredictionService(ITransformer model)
        {
            _mlContext = new MLContext();
            _model = model;
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }

        public SentimentType PredictSentiment(string text)
        {
            var prediction = _predictionEngine.Predict(new SentimentData { SentimentText = text });
            return (SentimentType)prediction.PredictedLabel;  // Convertir la etiqueta numérica predicha de nuevo a su tipo de sentimiento
        }
    }
}
