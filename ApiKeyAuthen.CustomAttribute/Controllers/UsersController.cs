using ApiKeyAuthen.CustomAttribute.CustomAttributes;
using ApiKeyAuthen.CustomAttribute.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthen.CustomAttribute.Controllers
{
  [ApiKey]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    public UsersController() { }

    [HttpGet]
    [Route("allusers")]
    public IActionResult GetUsers()
    {
      return Ok(DataGenerator.Users.ToList());
    }
  }
}
