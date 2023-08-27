using EnglishHelperService.Business;
using EnglishHelperService.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{
	[Description("User management")]
	[Route("[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<User> GetUserById(long id)
		{
			return await _userService.ReadUserById(id);
		}

		[HttpGet]
		public async Task<IEnumerable<User>> GetUsers()
		{
			return await _userService.ListUser();
		}

		[HttpPost]
		public void CreateUser([FromBody] CreateUserRequest request)
		{
			  _userService.Create(request);
		}

		[HttpPost]
		public void CreateUser([FromBody] UpdateUserRequest request)
		{
			_userService.Update(request);
		}

		[HttpDelete]
		public void DeleteUser(long id)
		{
			_userService.Delete(id);
		}
	}
}