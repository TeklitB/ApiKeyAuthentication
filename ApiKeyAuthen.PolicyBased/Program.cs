
using ApiKeyAuthen.PolicyBased.AuthorRequirements;
using ApiKeyAuthen.PolicyBased.Handlers;
using ApiKeyAuthen.SharedApiKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.APIKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using ApiKeyAuthen.SharedApiKeyValidator.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ApiKeyAuthen.PolicyBased
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer();
      DataGenerator.InitBogusData();
      builder.Services.AddControllers();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
      builder.Services.AddScoped<IApiKeyValidation, ApiKeyValidation>();
      // Used to access the HttpContext during policy-based api key authentication
      builder.Services.AddHttpContextAccessor();
      builder.Services.AddAuthorization(options =>
      {
        options.AddPolicy(Constants.ApiKeyPolicy, policy =>
        {
          policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
          policy.Requirements.Add(new ApiKeyRequirement());
        });
      });
      builder.Services.AddScoped<IAuthorizationHandler, ApiKeyHandler>();

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

      app.Run();
    }
  }
}
