namespace PrompSentiments.Domain.Models
{
    public class SentimentViewModel
    {
        public string InputText { get; set; } // El texto que el usuario introdujo
        public string ResponseMessage { get; set; } // El mensaje generado con base en el sentimiento
        public string ErrorMessage { get; set; } // Mensaje de error en caso de que el texto esté vacío
        public uint Sentiment { get; set; } // El valor del sentimiento (puede ser un enum convertido a uint)
    }
}
