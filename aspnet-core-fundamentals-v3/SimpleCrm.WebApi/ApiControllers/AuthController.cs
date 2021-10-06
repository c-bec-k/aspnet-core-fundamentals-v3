using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Auth;
using SimpleCrm.WebApi.Models;
using System.Linq;

namespace SimpleCrm.WebApi.ApiControllers
{
  [Route("auth")]
  public class AuthController : Controller
  {
    private readonly UserManager<CrmUser> _userManager;
    private readonly JwtFactory _jwtFactory;

    public AuthController(UserManager<CrmUser> userManager, JwtFactory jwtFactory)
    {
      this._userManager = userManager;
      this._jwtFactory = jwtFactory;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody] CredentialsViewModel credentials)
    { 
      if (!ModelState.IsValid)
      {
        return UnprocessableEntity(ModelState);
      }

      var user = await Authenticate(credentials.EmailAddress, credentials.Password);
      if (user == null)
      {
        return UnprocessableEntity("Invalid username or password");
      }

      // TODO: add GetUserData method
      var userModel = await GetUserData(user);

      // returns a UserSummaryViewModel containing a JWT and other user info
      return Ok(userModel);
    }

    private async Task<CrmUser> Authenticate(string emailAddress, string password)
    {
      if (emailAddress == "" || password == "")
      {
        return null;
      }

      var currentUser = await _userManager.FindByEmailAsync(emailAddress);
      if (currentUser == null)
      {
        var rand = new Random(DateTime.Now.Second).Next(2, 38);
        await Task.Delay(rand);
        return null;
      }

      var isUser = await _userManager.CheckPasswordAsync(currentUser, password);
      if (isUser)
      {
        return currentUser;
      }

      return null;
    }

    private async Task<UserSummaryViewModel> GetUserData(CrmUser user)
    {
      if (user == null) return null;

      var roles = await _userManager.GetRolesAsync(user);
      if (roles.Count == 0)
      {
        roles.Add("prospect");
      }

      var jwt = await _jwtFactory.GenerateEncodedToken(user.UserName, _jwtFactory.GenerateClaimsIdentity(user.UserName, user.Id.ToString()));
      var userModel = new UserSummaryViewModel
      {
        Id = user.Id,
        Name = user.UserName,
        EmailAddress = user.Email,
        Jwt = jwt,
        Roles = roles.ToArray()
      };

      return userModel;
    }
  }
}
