using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using EnglishHelperService.Business;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{

    [Description("Word management")]
    [ServiceFilter(typeof(LogUserActivity))]
    public class WordController : BaseApiController
    {
        private readonly IWordService _service;

        public WordController(IWordService service, ErrorLogger logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Get word by id
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
        /// Get User's words by user id.
        /// </summary>
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var userId = GetLoginedUserId();

            var response = await _service.List(userId);
            if (response.HasError)
            {
                LogError("userId: " + userId, response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Create word
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateWordRequest request)
        {
            request.UserId = GetLoginedUserId();

            var response = await _service.Create(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return CreatedAtAction("Create", new { id = response.Result.Id }, response.Result);
        }

        /// <summary>
        /// Update word
        /// </summary>
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateWordRequest request)
        {
            request.UserId = GetLoginedUserId();

            var response = await _service.Update(request);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Delete word
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _service.Delete(id);
            if (response.HasError)
            {
                LogError("Id: " + id, response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }
    }
}