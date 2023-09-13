﻿using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishHelperService.API.Controllers
{
	[Authorize]
	[ApiController]
	[ServiceFilter(typeof(LogUserActivity))]
	[Route("[controller]")]
	public class BaseApiController : ControllerBase
	{
		/// <summary>
		/// Get logined user id from ClaimTypes
		/// </summary>
		protected long GetLoginedUserId()
		{
			return User.GetUserId();
		}

		/// <summary>
		/// Get logined user's name from ClaimTypes
		/// </summary>
		protected string GetLoginedUsername()
		{
			return User.GetUsername();
		}
	}
}
