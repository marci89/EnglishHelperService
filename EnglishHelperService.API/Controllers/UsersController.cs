using EnglishHelperService.API.Extensions;
using EnglishHelperService.Business;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{
	[Description("User management")]
	public class UsersController : BaseApiController
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		/// <summary>
		/// Get user by id
		/// </summary>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(long id)
		{
			var response = await _userService.ReadUserById(id);
			if (response.HasError)
			{
				return this.CreateErrorResponse(response);
			}
			return Ok(response.Result);
		}

		/// <summary>
		/// Get paginated users list with filter request.
		/// Only admin role can use it.
		/// </summary>
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

		/// <summary>
		/// Update user
		/// </summary>
		[HttpPut()]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
		{
			var response = await _userService.Update(request);
			if (response.HasError)
			{
				return this.CreateErrorResponse(response);
			}
			return NoContent();
		}

		/// <summary>
		/// Delete user
		/// </summary>
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