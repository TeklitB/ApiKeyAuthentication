using ApiKeyAuthen.SharedApiKeyValidator.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApiKeyAuthen.SharedApiKeyValidator.APIKeyValidator
{
  public class ApiKeyValidation : IApiKeyValidation
  {
    private readonly IConfiguration _config;

    public ApiKeyValidation(IConfiguration config)
    {
      _config = config;
    }

    public bool IsValidApiKey(string appApiKey)
    {
      if (string.IsNullOrWhiteSpace(appApiKey))
        return false;

      // install Microsoft.Extensions.Configuration.Binder
      // GetValue is an extension method 
      string apiKey = _config.GetValue<string>(Constants.ApiKeyName);

      if (apiKey == null || apiKey != appApiKey)
        return false;

      return true;
    }
  }
}
