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
        /// Get User's learn statistics by logined user id.
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
        /// Get User's learn statistics by logined user id and filter for chart diagram.
        /// </summary>
        [HttpGet("ListForChart")]
        public async Task<IActionResult> ListForChart([FromQuery] ListLearnStatisticsChartRequest request)
        {
            var userId = GetLoginedUserId();

            var response = await _service.ListForChart(request, userId);
            if (response.HasError)
            {
                LogError("userId: " + userId, response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Create learn statistics by logined user id
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateLearnStatisticsRequest request)
        {
            var userId = GetLoginedUserId();

            var response = await _service.Create(request, userId);
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
        /// Delete All by logined user id
        /// </summary>
        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> Delete()
        {
            var userId = GetLoginedUserId();

            var response = await _service.DeleteAll(userId);
            if (response.HasError)
            {
                LogError("UserId: " + userId, response);
                return this.CreateErrorResponse(response);
            }
            return NoContent();
        }
    }
}