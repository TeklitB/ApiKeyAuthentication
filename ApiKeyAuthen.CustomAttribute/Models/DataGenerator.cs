using Bogus;

namespace ApiKeyAuthen.CustomAttribute.Models
{
  public static class DataGenerator
  {
    public const int NumberOfUsers = 10;
    public static readonly List<User> Users = new();

    public static void InitBogusData()
    {
      var data = GeUserGenerator();
      Users.AddRange(data.Generate(NumberOfUsers));
    }

    private static Faker<User> GeUserGenerator()
    {
      return new Faker<User>()
          .RuleFor(e => e.Id, _ => Guid.NewGuid())
          .RuleFor(e => e.Name, f => f.Name.FullName())
          .RuleFor(e => e.Address, f => f.Address.FullAddress())
          .RuleFor(e => e.Email, (f, e) => f.Internet.Email(e.Name))
          .RuleFor(e => e.Dob, f => f.Date.Future(10, DateTime.Now));

    }
  }
}
