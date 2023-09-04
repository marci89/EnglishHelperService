using EnglishHelperService.API.Extensions;
using EnglishHelperService.Business;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{

	[Description("Account management")]
	[Route("[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
		{
			var response = await _userService.Create(request);
			if (response.HasError)
			{
				return this.CreateErrorResponse(response);
			}
			return CreatedAtAction("Register", new { id = response.Result.Id }, response.Result);
		}


		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
		{
			var response = await _userService.Login(request);
			if (response.HasError)
			{
				return this.CreateErrorResponse(response);
			}
			return Ok(response.Result);
		}
	}
}
