using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;

namespace SentimentModelTrainer
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public string SentimentText { get; set; }

        [LoadColumn(1)]
        public string Sentiment { get; set; }
    }

    public class SentimentPrediction
    {
        public string SentimentText { get; set; }
        public string PredictedLabel { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear un contexto de ML.NET
            var mlContext = new MLContext();

            // Cargar los datos de entrenamiento desde un archivo TSV
            var data = mlContext.Data.LoadFromTextFile<SentimentData>("sentiment_data.tsv", separatorChar: '\t', hasHeader: true);

            // Crear un pipeline para preprocesar los datos y entrenar el modelo
            var pipeline = mlContext.Transforms.Text.FeaturizeText("SentimentText")
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Sentiment"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Sentiment", "SentimentText"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Entrenar el modelo
            var model = pipeline.Fit(data);

            // Guardar el modelo entrenado en un archivo
            mlContext.Model.Save(model, data.Schema, "sentiment_model.zip");

            Console.WriteLine("Modelo entrenado y guardado como 'sentiment_model.zip'");
        }
    }
}
