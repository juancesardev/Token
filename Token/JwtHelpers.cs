using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwTokens.Token
{
    public static class JwtHelpers
    {
		public static IEnumerable<Claim> Getclaims(this UserTokens userAccounts, Guid Id)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim("Id", userAccounts.Id.ToString()),
				new Claim(ClaimTypes.Name, userAccounts.UserName),
				new Claim(ClaimTypes.Email, userAccounts.EmailId),
				new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
				new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd yyyy HH:mm:ss tt")),
			};
			claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
			return claims;
		}



		//===================================
		public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, out Guid Id) //out Guid Id out int Id
		{
			Id = Guid.NewGuid();
			return GetClaims(userAccounts, out Id); //*******

		}


		public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
		{
			try
			{
				var userToken = new UserTokens();
				if (model == null)
				{
					throw new ArgumentNullException(nameof(model));
				}

				// obtain secret key
				var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

				Guid Id;
				//int Id = 1; //-------------------- le cambie tipo Guid a int
				// expira en 1 dia
				DateTime expireTime = DateTime.UtcNow.AddDays(1);

				//Validar nuestro token
				userToken.Validity = expireTime.TimeOfDay;

				//generar el JWT
				var jwToken = new JwtSecurityToken(
					issuer: jwtSettings.ValidIssuer,
					audience: jwtSettings.ValidAudience,
					claims: GetClaims(model, out Id), //claims: GetClaims(model, out Id), claims: GetClaims(model), 
					notBefore: new DateTimeOffset(DateTime.Now).DateTime,
					expires: new DateTimeOffset(expireTime).DateTime,
					signingCredentials: new SigningCredentials(
						new SymmetricSecurityKey(key),
						SecurityAlgorithms.HmacSha256
					)
				);

				//Guid Ids = Guid.NewGuid();
				userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
				userToken.UserName = model.UserName;
				userToken.Id = model.Id;
				userToken.GuidId = Id; //--------------- cambio de Id a Ids y le agregue el gui autogenerado

				return userToken;


			}
			catch (Exception exception)
			{
				throw new Exception("Error JWT", exception);
			}
		}
	}
}
