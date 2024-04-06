namespace ApiKeyAuthen.SharedApiKeyValidator.Interfaces
{
  public interface IApiKeyValidation
  {
    bool IsValidApiKey(string apiKey);
  }
}
