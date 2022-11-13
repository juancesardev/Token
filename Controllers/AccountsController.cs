using JwTokens.Data;
using JwTokens.Models;
using JwTokens.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwTokens.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApiDBContext _db;
        private readonly JwtSettings _jwtSettings;
        public AccountsController(ApiDBContext db, JwtSettings jwtSettings)
        {
           _jwtSettings = jwtSettings;
            _db = db;
        }


		[HttpPost]
		public IActionResult GetToken(UserLogins userLogin)
		{
			try
			{
				var Token = new UserTokens();
				var Valid = _db.Users.Any(user => user.Name.Equals(userLogin.UserName)); //, StringComparison.OrdinalIgnoreCase
				if (Valid)
				{
					var user = _db.Users.FirstOrDefault(user => user.Name.Equals(userLogin.UserName)); //
					if (user != null) { Console.WriteLine("usuario conectado ==================================" + user.Email); }

					Token = JwtHelpers.GenTokenKey(new UserTokens()
					{
						UserName = user.Name,
						EmailId = user.Email,
						Id = user.Id,
						GuidId = Guid.NewGuid(),
					}, _jwtSettings);
				}
				else
				{
					return BadRequest("wrong password");
				}

				return Ok(Token);
			}
			catch (Exception ex)
			{
				throw new Exception("GetToken Error", ex);
			}

		}

		//admin
		[HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(User);
        }
    }
}
