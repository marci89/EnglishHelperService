using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using EnglishHelperService.Business;
using EnglishHelperService.Business.Settings;
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
        private readonly IApplicationSettings _appSettings;
        private readonly IRegisterEmailSender _registerEmailSender;
        private readonly IResetPasswordEmailSender _resetPasswordEmailSender;

        public AccountController(
            IUserService service,
            IApplicationSettings appSettings,
            IRegisterEmailSender registerEmailSender,
            IResetPasswordEmailSender resetPasswordEmailSender,
            ErrorLogger logger) : base(logger)
        {
            _service = service;
            _appSettings = appSettings;
            _registerEmailSender = registerEmailSender;
            _resetPasswordEmailSender = resetPasswordEmailSender;
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
                    RecipientEmail = request.Email,
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

        /// <summary>
        /// forgot password (Sending email for reset password)
        /// </summary>
        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var currentLanguage = GetCurrentLanguage();

            var response = await _service.ForgotPassword(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            else
            {
                var emailRequest = new ResetPasswordEmailSenderRequest
                {
                    Username = response?.Result?.Username,
                    ApplicationName = _appSettings.ApplicationName,
                    ResetPasswordLink = _appSettings.ClientDomain + "reset-password?token=" + response?.Result?.Token,
                    RecipientEmail = response?.Result?.Email,
                    Language = currentLanguage,
                };

                var emailResponse = await _resetPasswordEmailSender.ExecuteAsync(emailRequest);

                if (emailResponse.HasError)
                {
                    emailRequest.Body = "";

                    LogError(JsonConvert.SerializeObject(emailRequest), emailResponse);
                    return this.CreateErrorResponse(emailResponse);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// reset password
        /// </summary>
        [AllowAnonymous]
        [HttpPut("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {

            var response = await _service.ResetPassword(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }
    }
}
