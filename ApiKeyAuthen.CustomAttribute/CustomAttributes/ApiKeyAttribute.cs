using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthen.CustomAttribute.CustomAttributes
{
  public class ApiKeyAttribute : ServiceFilterAttribute
  {
    public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter))
    { }
  }
}
