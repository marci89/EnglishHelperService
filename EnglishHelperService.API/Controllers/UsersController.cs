using EnglishHelperService.API.Extensions;
using EnglishHelperService.Business;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{
	[Description("User management")]
	[Route("[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> ListUser([FromQuery] ListUserWithFilterRequest request)
		{
			var response = await _userService.ListUser(request);
			if (response.HasError)
			{
				return this.CreateErrorResponse(response);
			}
			return Ok(response.Result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUserById(long id)
		{
			var user = await _userService.ReadUserById(id);
			return Ok(user);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUser([FromBody] UpdateUserRequest request)
		{
			await _userService.Update(request);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(long id)
		{
			var response = await _userService.Delete(id);
			if (response.HasError)
			{
				return this.CreateErrorResponse(response);
			}
			return NoContent();
		}
	}
}