using Microsoft.ML;
using Microsoft.ML.Data;
using PrompSentiments.Domain.Models;

namespace PrompSentiments.Infrastructure
{

    public class SentimentAnalysisTrainer
    {
        private readonly MLContext _mlContext;

        public SentimentAnalysisTrainer()
        {
            _mlContext = new MLContext();
        }

        public ITransformer TrainModel(string tsvFilePath)
        {
            // Cargar los datos
            var data = _mlContext.Data.LoadFromTextFile<SentimentData>(tsvFilePath, separatorChar: '\t', hasHeader: true);

            // Crear el pipeline de entrenamiento
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.SentimentText))  // Convertir texto en características
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SentimentData.SentimentLabel))  // Convertir las etiquetas a valores numéricos
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))  // Algoritmo de clasificación
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel")));  // Convertir el valor numérico predicho de nuevo a su etiqueta original

            // Entrenar el modelo
            var model = pipeline.Fit(data);
            return model;
        }
    }

}
