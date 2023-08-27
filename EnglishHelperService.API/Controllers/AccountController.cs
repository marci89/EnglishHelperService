using EnglishHelperService.Business;
using EnglishHelperService.Business.Models;
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
		public async Task<ActionResult> Register([FromBody] CreateUserRequest request)
		{
			await _userService.CreateAsync(request);
			return Ok();
		}

		[HttpPost("login")]
		public async Task<ActionResult<LoginUserResponse>> Login([FromBody] LoginUserRequest request)
		{
			var result = await _userService.Login(request);
			return Ok(result);
		}

	}

}
