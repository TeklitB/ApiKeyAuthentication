using ApiKeyAuthen.SharedApiKeyValidator.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthen.CustomMiddle.Controllers
{
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
