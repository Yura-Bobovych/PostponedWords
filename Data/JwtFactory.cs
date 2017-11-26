using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PostponedWords.Data;


namespace PostponedWords.Data
{
    public class JwtFactory : IJwtFactory
	{
		private readonly PostponedWords.Models.JwtIssuerOptions _jwtOptions;
		private readonly AppSettings Сonfiguration;

		public JwtFactory(IOptions<PostponedWords.Models.JwtIssuerOptions> jwtOptions, IOptions<AppSettings> configuration)
		{
			_jwtOptions = jwtOptions.Value;
			Сonfiguration = configuration.Value;
		}
		public ClaimsIdentity GenerateClaimsIdentity(string userName, int id)
		{
			return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
			{
				new Claim(Сonfiguration.JwtClaimIdentifiers.Id, id.ToString()),
				new Claim(Сonfiguration.JwtClaimIdentifiers.Rol, Сonfiguration.JwtClaims.BaseUser)
			});

		}
		public async Task<string> GenerateEncodedToken(string email, ClaimsIdentity identity)
		{
			var claims = new[]
			{
	  new Claim(JwtRegisteredClaimNames.Sub, email),
	  new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
	  new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
	  identity.FindFirst("rol"),
	  identity.FindFirst("id")
   };

			// Create the JWT security token and encode it.
			var jwt = new JwtSecurityToken(
				issuer: _jwtOptions.Issuer,
				audience: _jwtOptions.Audience,
				claims: claims,
				notBefore: _jwtOptions.NotBefore,
				expires: _jwtOptions.Expiration,
				signingCredentials: _jwtOptions.SigningCredentials);

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return encodedJwt;
		}

		private static long ToUnixEpochDate(DateTime date)
			 => (long)Math.Round((date.ToUniversalTime() -
								  new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
								 .TotalSeconds);

		private static void ThrowIfInvalidOptions(PostponedWords.Models.JwtIssuerOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));

			if (options.ValidFor <= TimeSpan.Zero)
			{
				throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(PostponedWords.Models.JwtIssuerOptions.ValidFor));
			}

			if (options.SigningCredentials == null)
			{
				throw new ArgumentNullException(nameof(PostponedWords.Models.JwtIssuerOptions.SigningCredentials));
			}

			if (options.JtiGenerator == null)
			{
				throw new ArgumentNullException(nameof(PostponedWords.Models.JwtIssuerOptions.JtiGenerator));
			}
		}
	}
}
