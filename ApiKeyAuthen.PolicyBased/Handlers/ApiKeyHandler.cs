using ApiKeyAuthen.PolicyBased.AuthorRequirements;
using ApiKeyAuthen.SharedApiKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ApiKeyAuthen.PolicyBased.Handlers
{
  public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
  {
    private readonly ILogger<ApiKeyHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyHandler(
      ILogger<ApiKeyHandler> logger,
      IHttpContextAccessor httpContextAccessor,
      IApiKeyValidation apiKeyValidation)
    {
      _logger = logger;
      _httpContextAccessor = httpContextAccessor;
      _apiKeyValidation = apiKeyValidation;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ApiKeyRequirement requirement)
    {
      _logger.LogInformation($"{nameof(ApiKeyHandler)}");
      string apiKey = _httpContextAccessor?
        .HttpContext?
        .Request
        .Headers[Constants.ApiKeyHeaderName]
        .ToString();

      if (string.IsNullOrWhiteSpace(apiKey))
      {
        _logger.LogError("Api key is invalid.");
        context.Fail();
        return Task.CompletedTask;
      }

      if (!_apiKeyValidation.IsValidApiKey(apiKey))
      {
        _logger.LogError("Unauthorized, Api key is invalid.");
        context.Fail();
        return Task.CompletedTask;
      }

      context.Succeed(requirement);

      return Task.CompletedTask;
    }
  }
}
