using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrompSentiments.Application.Services;
using PrompSentiments.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddControllersWithViews();  // Esto agrega MVC con Razor Pages
builder.Services.AddScoped<ISentimentAnalysisService, SentimentAnalysisService>();  // Registra el servicio para an�lisis de sentimientos

var app = builder.Build();

// Configuraci�n del pipeline de la aplicaci�n
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sentiment}/{action=AnalyzeSentiment}/{id?}");  // Rutas para controladores MVC

app.Run();
