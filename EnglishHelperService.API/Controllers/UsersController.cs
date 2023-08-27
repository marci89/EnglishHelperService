using EnglishHelperService.Business;
using EnglishHelperService.Business.Models;
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

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsersAsync()
		{
			var users = await _userService.ListUserAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUserByIdAsync(long id)
		{
			var user = await _userService.ReadUserByIdAsync(id);
			return Ok(user);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserRequest request)
		{
			await _userService.UpdateAsync(request);
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteUserAsync(long id)
		{
			await _userService.DeleteAsync(id);
			return Ok();
		}
	}
}