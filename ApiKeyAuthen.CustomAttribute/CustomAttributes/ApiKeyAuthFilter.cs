using ApiKeyAuthen.SharedApiKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiKeyAuthen.CustomAttribute.CustomAttributes
{
  public class ApiKeyAuthFilter : IAuthorizationFilter
  {
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
    {
      _apiKeyValidation = apiKeyValidation;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      string appApiKey = context
        .HttpContext
        .Request
        .Headers[Constants.ApiKeyHeaderName]
        .ToString();

      if (string.IsNullOrWhiteSpace(appApiKey))
      {
        context.Result = new BadRequestResult();
        return;
      }

      if (!_apiKeyValidation.IsValidApiKey(appApiKey))
        context.Result = new UnauthorizedResult();
    }
  }
}
