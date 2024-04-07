using ApiKeyAuthen.SharedApiKeyValidator;
using ApiKeyAuthen.SharedApiKeyValidator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyAuthen.PolicyBased.Controllers
{
  [Authorize(Policy = Constants.ApiKeyPolicy)]
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    public UsersController() { }

    [HttpGet]
    [Route("allUsers")]
    public IActionResult GetUsers()
    {
      return Ok(DataGenerator.Users.ToList());
    }
  }
}
