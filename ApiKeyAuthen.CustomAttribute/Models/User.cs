using System.Text.Json;

namespace ApiKeyAuthen.CustomAttribute.Models
{
  public class User
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime Dob { get; set; }

    public string Email { get; set; }
    public bool IsActived
    {
      get; set;
    }
    public override string ToString()
    {
      return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
    }
  }
}
