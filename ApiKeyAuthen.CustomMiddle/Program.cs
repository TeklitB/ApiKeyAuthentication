
using ApiKeyAuthen.CustomMiddle.CustomMiddleware;
using ApiKeyAuthen.SharedApiKeyValidator.APIKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using ApiKeyAuthen.SharedApiKeyValidator.Models;

namespace ApiKeyAuthen.CustomMiddle
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      DataGenerator.InitBogusData();
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
      builder.Services.AddSingleton<IApiKeyValidation, ApiKeyValidation>();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseMiddleware<ApiKeyMiddleware>();

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.Run();
    }
  }
}
