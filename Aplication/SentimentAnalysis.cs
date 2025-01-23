using Microsoft.ML;
using Microsoft.ML.Data;
using PrompSentiments.Domain.Models;
using System.Collections.Generic;


namespace PrompSentiments.Aplication
{

    public class SentimentAnalysis
    {
        private readonly MLContext _mlContext;

        public SentimentAnalysis()
        {
            _mlContext = new MLContext();
        }

        public ITransformer TrainModel(string dataFilePath)
        {
            // Cargar datos
            var data = _mlContext.Data.LoadFromTextFile<SentimentData>(dataFilePath, separatorChar: ',', hasHeader: true);

            // Preprocesamiento de texto
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.SentimentText))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(SentimentData.SentimentLabel), featureColumnName: "Features"));

            // Entrenar el modelo
            var model = pipeline.Fit(data);
            return model;
        }
    }
}

