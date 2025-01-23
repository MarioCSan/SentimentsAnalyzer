using Microsoft.ML;
using PrompSentiments.Application.Interfaces;
using PrompSentiments.Domain.Models;
using System.Reflection;

public class SentimentAnalysisService : ISentimentAnalysisService
{
    private readonly MLContext _mlContext;
    private ITransformer _model;
    private PredictionEngine<SentimentData, SentimentPrediction> _predictionEngine;

    public SentimentAnalysisService(MLContext mlContext)
    {
        _mlContext = mlContext;
        LoadModel();
    }

    public void LoadModel()
    {
        string basePath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        string dataPath = Path.Combine(basePath, "wikipedia-detox-250-line-data.tsv");

        if (System.IO.File.Exists("sentiment_model.zip"))
        {
            // Cargar el modelo existente
            _model = _mlContext.Model.Load("sentiment_model.zip", out var modelInputSchema);
            _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        }
        else
        {
            if (File.Exists(dataPath))
            {
                Console.WriteLine("El archivo existe y se procederá a entrenar el modelo.");
                TrainModel(dataPath);
            }
            else
            {
                Console.WriteLine("El archivo de datos no se encuentra.");
            }
        }
    }
    public void TrainModel(string dataPath)
    {
        // Cargar los datos desde el archivo
        IDataView data = _mlContext.Data.LoadFromTextFile<SentimentData>(dataPath, separatorChar: '\t', hasHeader: true);

        // Definir el pipeline de ML.NET
        var pipeline = _mlContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: "SentimentText")
            .Append(_mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Label", inputColumnName: "Sentiment"))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(labelColumnName: "Label", featureColumnName: "Features"))
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: "PredictedLabel", inputColumnName: "Label"));

        // Entrenamiento del modelo
        _model = pipeline.Fit(data);
        _mlContext.Model.Save(_model, data.Schema, "sentiment_model.zip");

        // Crear el motor de predicción
        _predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
    }

    public SentimentPrediction PredictSentiment(string text)
    {
        var sentimentData = new SentimentData
        {
            SentimentText = text
        };

        SentimentPrediction prediction = null;

        try
        {
            if (_predictionEngine == null)
            {
                throw new InvalidOperationException("El motor de predicción no ha sido inicializado.");
            }

            // Realizar la predicción
            prediction = _predictionEngine.Predict(sentimentData);
        }
        catch (Exception ex)
        {
            // Manejo de excepciones: en caso de error, lanzar una excepción con un mensaje claro
            throw new Exception($"An error occurred while predicting sentiment: {ex.Message}");
        }

        // Devolver la predicción si es válida
        return prediction;
    }


    // Implementación de AnalyzeSentiment
    public SentimentPrediction AnalyzeSentiment(string text)
    {
        return PredictSentiment(text);  // Puedes reutilizar el método PredictSentiment
    }

    // Implementación de GenerateResponse
    public string GenerateResponse(SentimentType sentiment)
    {
        return sentiment switch
        {
            SentimentType.Angry => "We understand your frustration. We're here to help!",
            SentimentType.Happy => "We're glad you're feeling good! Keep it up!",
            _ => "We're here for you, no matter how you're feeling."
        };
    }
}
