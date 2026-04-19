using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identity.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSE.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Configuration.IdentityOptions _identityOpts;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<Configuration.IdentityOptions> identityOpts)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityOpts = identityOpts.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                var token = await GerarJwt(user.Email);

                return Ok(token);
            }

            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                var token = await GerarJwt(userLogin.Email);

                return Ok(token);
            }

            return BadRequest();
        }


        private async Task<UserLoginResponse> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            (var claims, var userRoles) = await BuildClaimsAsync(user);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_identityOpts.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _identityOpts.Issuer,
                Audience = _identityOpts.ValidIn,
                Subject = identityClaims, // Dados do usuário, que foram definidas nas claims
                Expires = DateTime.UtcNow.AddHours(_identityOpts.ExpirationTimeInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return new UserLoginResponse()
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_identityOpts.ExpirationTimeInHours).TotalSeconds,
                UserToken = new UserToken()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim() { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return new DateTimeOffset(date.ToUniversalTime()).ToUnixTimeSeconds();
        }

        private async Task<(IList<Claim> Claims, IList<string> Roles)> BuildClaimsAsync(IdentityUser user)
        {
            var claimsAsync = _userManager.GetClaimsAsync(user);
            var userRolesAsync = _userManager.GetRolesAsync(user);

            await Task.WhenAll(claimsAsync, userRolesAsync);

            var claims = claimsAsync.Result;
            var userRoles = userRolesAsync.Result;

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var utcNow = DateTime.UtcNow;

            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(utcNow).ToString())); // Quando vai expirar.
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(utcNow).ToString(), ClaimValueTypes.Integer64)); // Quando foi emitido. Issue at.

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            return (claims, userRoles);
        }
    }
}
