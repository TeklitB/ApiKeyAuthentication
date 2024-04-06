using ApiKeyAuthen.CustomAttribute.Commons;
using ApiKeyAuthen.CustomAttribute.Interfaces;

namespace ApiKeyAuthen.CustomAttribute.APIKeyValidator
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

      string apiKey = _config.GetValue<string>(Constants.ApiKeyName);

      if (apiKey == null || apiKey != appApiKey)
        return false;

      return true;
    }
  }
}
