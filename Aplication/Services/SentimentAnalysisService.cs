using Microsoft.ML;
using PrompSentiments.Domain.Models;
using Microsoft.ML.Data;
using System;
using PrompSentiments.Application.Interfaces;

namespace PrompSentiments.Application.Services
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

        public SentimentAnalysisService()
        {
            _mlContext = new MLContext();
            // Aquí cargamos el modelo previamente entrenado desde el archivo
            _model = _mlContext.Model.Load("sentiment_model.zip", out var modelInputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }

        public SentimentPrediction AnalyzeSentiment(string text)
        {
            var sentimentData = new SentimentData { SentimentText = text };
            var prediction = _predictionEngine.Predict(sentimentData);

            return new SentimentPrediction
            {
                SentimentText = text,
                Sentiment = prediction.Sentiment,
                PredictedLabel = prediction.PredictedLabel,
                Probability = prediction.Probability,
                Score = prediction.Score
            };
        }

        public string GenerateResponse(SentimentType sentiment)
        {
            switch (sentiment)
            {
                case SentimentType.Happy:
                    return "So glad you are happy! :)";
                case SentimentType.Angry:
                    return "I'm sorry you're angry. Do you want to talk about it?";
                case SentimentType.Neutral:
                    return "I understand. How can I help you today?";
                default:
                    return "I can't identify the feeling.";
            }
        }
    }
}
