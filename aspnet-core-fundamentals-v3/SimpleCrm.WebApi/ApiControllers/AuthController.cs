using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleCrm.WebApi.Auth;
using SimpleCrm.WebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace SimpleCrm.WebApi.ApiControllers
{
  [Route("auth")]
  public class AuthController : Controller
  {
    private readonly UserManager<CrmUser> _userManager;
    private readonly IJwtFactory _jwtFactory;
    private readonly MSAuthSettings _microsoftAuthSettings;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(UserManager<CrmUser> userManager, IJwtFactory jwtFactory,
    MSAuthSettings microsoftAuthSettings, IConfiguration configuration, ILogger<AuthController> logger)
    {
      this._configuration = configuration;
      this._microsoftAuthSettings = microsoftAuthSettings;
      this._userManager = userManager;
      this._jwtFactory = jwtFactory;
      this._logger = logger;
    }


    // 
    // Step 6: Enable OAuth - JWT Login with Microsoft
    // 

    [HttpGet("external/microsoft")]
    public IActionResult GetMicrosoft()
    {
      return Ok(new
      {
        client_id = _microsoftAuthSettings.ClientId,
        scope = "https://graph.microsoft.com/user.read",
        state = ""
      });
    }

    [HttpPost("external/microsoft")]
    public async Task<IActionResult> PostMicrosoft([FromBody] MicrosoftAuthViewModel model)
    {
      var verifier = new MicrosoftAuthVerifier<AuthController>(_microsoftAuthSettings, _configuration["HttpHost"] + (model.BaseHref ?? "/"), _logger);
      var profile = await verifier.AcquireUser(model.AccessToken);

      // TODO: validate the 'profile' object is successful, and email address is included

      if (profile.IsSuccessful == false)
      {
        return StatusCode(StatusCodes.Status400BadRequest, profile.Error.Message);
      }

      if (String.IsNullOrWhiteSpace(profile.Mail))
      {
        return StatusCode(StatusCodes.Status403Forbidden, "Email address is required");
      }

      var UserLoginInfo = new UserLoginInfo("Microsoft", profile.Id, profile.DisplayName);

      var user = await _userManager.FindByEmailAsync(profile.Mail);
      if (user == null)
      {

        user = new CrmUser
        {
          DisplayName = profile.DisplayName,
          Email = profile.Mail,
          PhoneNumber = profile.MobilePhone,
          UserName = profile.Mail
        };

        await _userManager.CreateAsync(user);
      }

      var userModel = await GetUserData(user);
      return Ok(userModel);
    }


    // 
    // Step 7 Allow clients to refresh login info + JWT
    // 

    [Authorize(Policy = "ApiUser")]
    [HttpPost("verify")] // POST api/auth/verify
    public async Task<IActionResult> Verify()
    {
      if (User.Identity.IsAuthenticated)
      {
        var user = await _userManager.FindByEmailAsync(User.Identity.Name);
        if (user == null)
          return Forbid();
        var userModel = await GetUserData(user);
        return new ObjectResult(userModel);
      }

      return Forbid();
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


      var userModel = await GetUserData(user);


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
        // random number of ms to deter timing attacks
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
