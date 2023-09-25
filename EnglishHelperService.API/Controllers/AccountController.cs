using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using EnglishHelperService.Business;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{

    [Description("Account management")]
    public class AccountController : BaseApiController
    {
        private readonly IUserService _service;
        private readonly IRegisterEmailSender _registerEmailSender;

        public AccountController(
            IUserService service,
            IRegisterEmailSender registerEmailSender,
            ErrorLogger logger) : base(logger)
        {
            _service = service;
            _registerEmailSender = registerEmailSender;
        }

        /// <summary>
        /// User create (registration)
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            var currentLanguage = GetCurrentLanguage();
            var response = await _service.Create(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            else
            {
                await _registerEmailSender.ExecuteAsync(new RegisterEmailSenderRequest
                {
                    Username = request.Username,
                    // RecipientEmail = request.Email,
                    RecipientEmail = "kismarczirobi@gmail.com",
                    Language = currentLanguage,
                });
            }
            return CreatedAtAction("Register", new { id = response.Result.Id }, response.Result);
        }

        /// <summary>
        /// Login user and auth with jwt token
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var response = await _service.Login(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Change logined user email
        /// </summary>
        [HttpPut("changeEmail")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
        {
            request.Id = GetLoginedUserId();

            var response = await _service.ChangeEmail(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }

        /// <summary>
        /// Change logined user password
        /// </summary>
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            request.Id = GetLoginedUserId();

            var response = await _service.ChangePassword(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }
    }
}
