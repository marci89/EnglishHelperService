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
    [Description("User management")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _service;

        public UserController(IUserService service, ErrorLogger logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await _service.ReadById(id);
            if (response.HasError)
            {
                LogError("Id: " + id, response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Get paginated users list with filter request.
        /// Only admin role can use it.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("List")]
        public async Task<IActionResult> List([FromQuery] ListUserWithFilterRequest request)
        {
            var response = await _service.List(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Update user
        /// </summary>
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            var response = await _service.Update(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _service.DeleteById(id);
            if (response.HasError)
            {
                LogError("Id: " + id, response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }
    }
}