
using ApiKeyAuthen.EndpointFilter.EndpointFilters;
using ApiKeyAuthen.SharedApiKeyValidator.APIKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using ApiKeyAuthen.SharedApiKeyValidator.Models;

namespace ApiKeyAuthen.EndpointFilter
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
      builder.Services.AddScoped<IApiKeyValidation, ApiKeyValidation>();

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();


      app.MapControllers();

      app.MapGet("api/Users/allUsers", () =>
      {
        return Results.Ok(DataGenerator.Users.ToArray());
      }).AddEndpointFilter<ApiKeyEndpointFilter>();

      app.Run();
    }
  }
}
