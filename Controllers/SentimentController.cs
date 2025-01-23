using Microsoft.AspNetCore.Mvc;
using PrompSentiments.Application.Interfaces;
using PrompSentiments.Domain.Models;

namespace PrompSentiments.Web.Controllers
{
    public class SentimentController : Controller
    {
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public SentimentController(ISentimentAnalysisService sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        // GET: Sentiment/AnalyzeSentiment
        public IActionResult AnalyzeSentiment()
        {
            return View("~/Views/Sentiment/AnalyzeSentiment.cshtml"); // Devuelve la vista 'AnalyzeSentiment.cshtml'
        }

        // POST: Sentiment/AnalyzeSentiment
        [HttpPost]
        public IActionResult AnalyzeSentiment(string text)
        {
            var prediction = _sentimentAnalysisService.AnalyzeSentiment(text);
            var response = _sentimentAnalysisService.GenerateResponse(prediction.PredictedLabel);

            ViewData["Response"] = response; 
            return View(prediction); 
        }
    }
}
