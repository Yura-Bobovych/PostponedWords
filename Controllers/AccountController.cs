using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostponedWords.Data;
using System.Security.Claims;
using Newtonsoft.Json;
using PostponedWords.Models;
using Microsoft.Extensions.Options;
using PostponedWords.Middleware;


namespace PostponedWords.Controllers
{
	[Produces("application/json")]
	[Route("api/[Controller]/[Action]")]
	public class AccountController : Controller
	{		
		private readonly PostponedWords.Data.IJwtFactory _jwtFactory;
		private readonly JwtIssuerOptions _jwtOptions;
		private readonly ILogger<AccountController> _logger;
		private readonly IDbUserMiddleware _userMw; 
		public AccountController(ILogger<AccountController> logger, IOptions<JwtIssuerOptions> jwtOptions,IJwtFactory jwtFactory,IDbUserMiddleware userMw)
		{
			_jwtFactory = jwtFactory;
			_jwtOptions = jwtOptions.Value;
			_logger = logger;
			_userMw = userMw;
		}

		public async Task<string> Index()
		{
			return "index";
		}

		[HttpPost]
		public async Task<IActionResult> SignIn([FromBody] EmailPost EmailObj)
		{
			
			_logger.LogDebug("sing in request with email: " + EmailObj.Email);			
			var identity = GetClaimsIdentity(EmailObj.Email);
			if (identity != null)
			{
				var response = new
				{
					auth_token = await _jwtFactory.GenerateEncodedToken(EmailObj.Email, identity),
					expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
				};				
				return new OkObjectResult(response);
			}
			return null;
		}

		private ClaimsIdentity GetClaimsIdentity(string email)
		{
			if (!string.IsNullOrEmpty(email))
			{

				if (_userMw.UserExist(email))
				{
					_logger.LogDebug("Sing in user: " + email);
					return _jwtFactory.GenerateClaimsIdentity(email, 1);
				}
				else
				{
					_userMw.CreateUserAsync(email);
					_logger.LogDebug("Created and sing in new user: " + email);
					return _jwtFactory.GenerateClaimsIdentity(email, 1);
				}
			}
			return null;
		}


		public class EmailPost
		{
			public string Email { get; set; }
		}
	}
}