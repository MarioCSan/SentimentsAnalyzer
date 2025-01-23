using Microsoft.AspNetCore.Mvc;
using PrompSentiments.Aplication.DTOs;
using PrompSentiments.Application.Interfaces;
using PrompSentiments.Domain.Models;
public class SentimentController : Controller
{
    private readonly ISentimentAnalysisService _sentimentAnalysisService;

    public SentimentController(ISentimentAnalysisService sentimentAnalysisService)
    {
        _sentimentAnalysisService = sentimentAnalysisService;
    }

    public IActionResult AnalyzeSentiment(string text)
    {
        // Crear un objeto para pasar los resultados y cualquier error a la vista
        var model = new PrompSentiments.Domain.Models.SentimentViewModel();

        if (string.IsNullOrEmpty(text))
        {
            // Si el texto está vacío, mostrar un mensaje de error en la misma vista
            model.ErrorMessage = "Text cannot be empty.";
            return View(model);  // Vuelve a la misma vista con el mensaje de error
        }

        // Si el texto no está vacío, realiza la predicción
        var prediction = _sentimentAnalysisService.AnalyzeSentiment(text);
        var sentimentType = (SentimentType)(uint)prediction.Sentiment;
        var responseMessage = _sentimentAnalysisService.GenerateResponse(sentimentType);

        // Pasar los resultados a la vista
        model.Sentiment = prediction.Sentiment;
        model.ResponseMessage = responseMessage;
        model.InputText = text;  // Mantener el texto ingresado para mostrarlo después

        return View(model);  // Devuelve la vista con los resultados
    }
}
