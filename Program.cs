using Microsoft.ML;
using PrompSentiments.Application.Interfaces;
using PrompSentiments.Application.Services;  // Asegúrate de incluir el espacio de nombres adecuado

var builder = WebApplication.CreateBuilder(args);

// Registrar MLContext como servicio
builder.Services.AddSingleton<MLContext>();  // Usamos Singleton, ya que MLContext se puede reutilizar

// Registrar otros servicios
builder.Services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sentiment}/{action=AnalyzeSentiment}/{text?}");

app.Run();
