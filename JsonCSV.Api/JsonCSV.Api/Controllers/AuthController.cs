using JsonCSV.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JsonCSV.Api.Controllers
{
	[Route("api/authController")]
	[ApiController]
	public class AuthController : Controller
	{
		private readonly ICityRepository _context;
		private readonly IConfiguration _configuration;

		public AuthController(ICityRepository context, IConfiguration config) { 
			_context = context;	
			_configuration = config;	
		}
		public class AuthModel {
			public string UserName { get; set; } = string.Empty;
			public string Password { get; set; } = string.Empty;

        }
		
		[HttpPost]
		public async Task<ActionResult<string>> Authentication(AuthModel authBody)
		{
			object user = await _context.Validate(authBody.UserName, authBody.Password);

			if (!(bool)user.GetType().GetProperty("status").GetValue(user))
			{
				return Unauthorized();
			}


			var SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AutenticationSecret:Secret"]));
			var signingCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
			var claimsToken = new List<Claim>
			{
				new Claim("sub", user.GetType().GetProperty("userId").GetValue(user).ToString()),
				new Claim("name",user.GetType().GetProperty("username").GetValue(user).ToString()),
				new Claim("Role",user.GetType().GetProperty("role").GetValue(user).ToString())
			};

			var jwtSecurityToken = new JwtSecurityToken(_configuration["AutenticationSecret:Issuer"],
														_configuration["AutenticationSecret:Audiencie"],
														claimsToken,
														DateTime.UtcNow,
														DateTime.UtcNow.AddHours(3000),
														signingCredentials);

			var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			return Ok(tokenToReturn);
		}
	}
}
