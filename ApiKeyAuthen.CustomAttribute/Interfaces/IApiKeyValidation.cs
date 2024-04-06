namespace ApiKeyAuthen.CustomAttribute.Interfaces
{
  public interface IApiKeyValidation
  {
    bool IsValidApiKey(string apiKey);
  }
}
