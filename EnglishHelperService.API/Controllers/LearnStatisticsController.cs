using EnglishHelperService.API.Extensions;
using EnglishHelperService.API.Helpers;
using EnglishHelperService.Business;
using EnglishHelperService.ServiceContracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EnglishHelperService.API.Controllers
{
    [Description("LearnStatistics management")]
    [ServiceFilter(typeof(LogUserActivity))]
    public class LearnStatisticsController : BaseApiController
    {
        private readonly ILearnStatisticsService _service;

        public LearnStatisticsController(ILearnStatisticsService service, ErrorLogger logger) : base(logger)
        {
            _service = service;
        }

        /// <summary>
        /// Get User's learn statistics by user id.
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
        /// Create learn statistics
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateLearnStatisticsRequest request)
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
        /// Delete learn statistics by id
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

        /// <summary>
        /// Delete All
        /// </summary>
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> Delete()
        {
            var id = GetLoginedUserId();

            var response = await _service.DeleteAll(id);
            if (response.HasError)
            {
                LogError("Id: " + id, response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }
    }
}