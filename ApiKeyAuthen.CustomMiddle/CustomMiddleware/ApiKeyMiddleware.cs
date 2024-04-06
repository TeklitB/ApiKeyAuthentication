using ApiKeyAuthen.SharedApiKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using System.Net;

namespace ApiKeyAuthen.CustomMiddle.CustomMiddleware
{
  public class ApiKeyMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
    {
      _next = next;
      _apiKeyValidation = apiKeyValidation;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      string appApiKey = context.Request.Headers[Constants.ApiKeyHeaderName];

      if (string.IsNullOrWhiteSpace(appApiKey))
      {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return;
      }

      if (!_apiKeyValidation.IsValidApiKey(appApiKey!))
      {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return;
      }

      // Invokes the next middleware component in the pipeline
      await _next(context);
    }
  }
}
