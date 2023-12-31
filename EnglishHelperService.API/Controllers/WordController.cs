﻿using ClosedXML.Excel;
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
        /// Get User's words by logined user id.
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
        /// Get User's words by logined user id and filtering.
        /// </summary>
        [HttpGet("ListWithFilter")]
        public async Task<IActionResult> ListWithFilter([FromQuery] ListWordWithFilterRequest request)
        {
            var userId = GetLoginedUserId();

            var response = await _service.ListWithFilter(request, userId);
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
        /// Update word
        /// </summary>
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] UpdateWordRequest request)
        {
            var userId = GetLoginedUserId();

            var response = await _service.Update(request, userId);
            if (response.HasError)
            {
                LogError(JsonConvert.SerializeObject(request), response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Update used word when learning
        /// </summary>
        [HttpPut("UpdateUsedWord")]
        public async Task<IActionResult> UpdateUsedWord([FromBody] UpdateUsedWordRequest request)
        {
            var response = await _service.UpdateUsedWord(request);
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

        /// <summary>
        /// Reset all word's CorrectCount and IncorrectCount property to 0
        /// </summary>
        [HttpPut("ResetResults")]
        public async Task<IActionResult> ResetResults()
        {
            var userId = GetLoginedUserId();

            var response = await _service.ResetResults(userId);
            if (response.HasError)
            {
                LogError("UserId: " + userId, response);
                return this.CreateErrorResponse(response);
            }
            return Ok(response.Result);
        }

        /// <summary>
        /// Export word list to txt file
        /// </summary>
        [HttpPost("ExportWordListToTextFile")]
        public async Task<IActionResult> ExportTextFile()
        {
            var userId = GetLoginedUserId();

            var response = await _service.ExportWordListToTextFile(userId);
            if (response.HasError)
            {
                LogError("userId: " + userId, response);
                return this.CreateErrorResponse(response);
            }

            // Set the response content type and headers
            Response.Headers.Add("Content-Disposition", "attachment; filename=exported-word-list.txt");
            return File(response.Result, "text/plain");
        }

        /// <summary>
        /// Import word list from txt file
        /// </summary>
        [HttpPost("ImportWordListFromTextFile")]
        public async Task<IActionResult> ImportTextFile()
        {
            var userId = GetLoginedUserId();

            try
            {
                var validationResult = IsValidUploadedFiles(new List<string> { ".txt" });
                if (validationResult.HasError)
                {
                    LogError("userId: " + userId, validationResult);
                    return this.CreateErrorResponse(validationResult);
                }

                var file = Request.Form.Files[0];

                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    //Get file content
                    var fileContent = await streamReader.ReadToEndAsync();

                    var response = await _service.ImportWordListFromTextFile(new ImportWordListFromTextFileRequest
                    {
                        UserId = userId,
                        Content = fileContent
                    });

                    if (response.HasError)
                    {
                        LogError("userId: " + userId, response);
                        return this.CreateErrorResponse(response);
                    }
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseBase
                {
                    ErrorMessage = ErrorMessage.UploadedFileFailed,
                    ExceptionErrorMessage = ex.Message
                };

                LogError("userId: " + userId, response);
                return StatusCode(500, response.ErrorMessage);
            }
        }

        /// <summary>
        /// Export word list to Excel file
        /// </summary>
        [HttpPost("ExportWordListToExcelFile")]
        public async Task<IActionResult> ExportExcelFile()
        {
            var userId = GetLoginedUserId();

            var response = await _service.ExportWordListToExcelFile(userId);
            if (response.HasError)
            {
                LogError("userId: " + userId, response);
                return this.CreateErrorResponse(response);
            }

            // Set the response content type and headers for Excel
            Response.Headers.Add("Content-Disposition", "attachment; filename=exported-word-list.xlsx");
            return File(response.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        /// <summary>
        /// Import word list from Excel file
        /// </summary>
        [HttpPost("ImportWordListFromExcelFile")]
        public async Task<IActionResult> ImportExcelFile()
        {
            var userId = GetLoginedUserId();

            try
            {
                var validationResult = IsValidUploadedFiles(new List<string> { ".xlsx", ".xls" });
                if (validationResult.HasError)
                {
                    LogError("userId: " + userId, validationResult);
                    return this.CreateErrorResponse(validationResult);
                }
                var file = Request.Form.Files[0];

                //using excel file
                using (var workbook = new XLWorkbook(file.OpenReadStream()))
                {
                    var response = await _service.ImportWordListFromExcelFile(new ImportWordListFromExcelFileRequest
                    {
                        UserId = userId,
                        Workbook = workbook
                    });
                    if (response.HasError)
                    {
                        LogError("userId: " + userId, response);
                        return this.CreateErrorResponse(response);
                    }
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                var response = new ResponseBase
                {
                    ErrorMessage = ErrorMessage.UploadedFileFailed,
                    ExceptionErrorMessage = ex.Message
                };

                LogError("userId: " + userId, response);
                return StatusCode(500, response.ErrorMessage);
            }
        }
    }
}