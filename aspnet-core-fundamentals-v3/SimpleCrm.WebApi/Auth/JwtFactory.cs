using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SimpleCrm.WebApi.Auth
{
  public class JwtFactory : IJwtFactory
  {
    private readonly JwtIssuerOptions _jwtOptions;

    public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
    {
      _jwtOptions = jwtOptions.Value;
      ThrowIfInvalidOptions(_jwtOptions);
    }

    public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
    {
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, userName),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat,
          ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
          ClaimValueTypes.Integer64),
       identity.FindFirst(Constants.JwtClaimIdentifiers.Rol),
       identity.FindFirst(Constants.JwtClaimIdentifiers.Id)
      };
    }



    public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
    {
      return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
      {
        new Claim(Constants.JwtClaimIdentifiers.Id, id),
        new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess)
      });
    }

    private static long toUnixEpochDate(DateTime date) =>
      (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970,1,1,0,0,0, TimeSpan.Zero)).TotalSeconds);

    private static void ThrowIfInvalidOptions(JwtIssuerOptions jwtOptions)
    {
      throw new NotImplementedException();
    }
  }
}
