using ClosedXML.Excel;
using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EnglishHelperService.Business
{
    /// <summary>
    /// Managing Words
    /// </summary>
    public class WordService : IWordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly WordFactory _factory;
        private readonly WordValidator _validator;

        public WordService(
            IUnitOfWork unitOfWork,
            WordFactory factory,
            WordValidator validator
            )
        {
            _unitOfWork = unitOfWork;
            _factory = factory;
            _validator = validator;
        }

        /// <summary>
        /// Read word by id
        /// </summary>
        public async Task<ReadWordByIdResponse> ReadById(long id)
        {
            try
            {
                var entity = await _unitOfWork.WordRepository.ReadAsync(u => u.Id == id);
                var result = _factory.Create(entity);
                if (result is null)
                {
                    return await _validator.CreateNotFoundResponse<ReadWordByIdResponse>();
                }

                return new ReadWordByIdResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ReadWordByIdResponse>(ex.Message);
            }
        }

        /// <summary>
        /// List words by user id
        /// </summary>
        public async Task<ListWordResponse> List(long userId)
        {
            try
            {
                var entities = await _unitOfWork.WordRepository.Query(x => x.UserId == userId).ToListAsync();

                return await Task.FromResult(new ListWordResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = entities.Select(x => _factory.Create(x)).ToList()
                });
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ListWordResponse>(ex.Message);
            }
        }

        /// <summary>
        /// Create word
        /// </summary>
        public async Task<CreateWordResponse> Create(CreateWordRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidCreateRequest(request);
                if (!validationResult.HasError)
                {
                    var entity = _factory.Create(request);
                    await _unitOfWork.WordRepository.CreateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new CreateWordResponse
                    {
                        StatusCode = StatusCode.Created,
                        Result = _factory.Create(entity)
                    };
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateCreationErrorResponse<CreateWordResponse>(ex.Message);
            }

        }

        /// <summary>
        /// Update word
        /// </summary>
        public async Task<UpdateWordResponse> Update(UpdateWordRequest request)
        {
            try
            {
                var validationResult = _validator.IsValidUpdateRequest(request);
                if (!validationResult.HasError)
                {
                    var entity = await _unitOfWork.WordRepository.ReadAsync(u => u.Id == request.Id);
                    if (entity is null)
                    {
                        return await _validator.CreateNotFoundResponse<UpdateWordResponse>();
                    }

                    entity.EnglishText = request.EnglishText;
                    entity.HungarianText = request.HungarianText;
                    entity.CorrectCount = request.CorrectCount;
                    entity.IncorrectCount = request.IncorrectCount;

                    await _unitOfWork.WordRepository.UpdateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new UpdateWordResponse
                    {
                        StatusCode = StatusCode.Ok,
                        Result = _factory.Create(entity)
                    };
                }

                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateUpdateErrorResponse<UpdateWordResponse>(ex.Message);
            }

        }

        /// <summary>
        /// Delete word by id
        /// </summary>
        public async Task<ResponseBase> Delete(long id)
        {
            try
            {
                var entity = await _unitOfWork.WordRepository.ReadAsync(u => u.Id == id);
                if (entity is null)
                {
                    return await _validator.CreateNotFoundResponse<ResponseBase>();
                }

                await _unitOfWork.WordRepository.DeleteAsync(entity);
                await _unitOfWork.SaveAsync();

                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return await _validator.CreateDeleteErrorResponse<ResponseBase>(ex.Message);
            }
        }

        /// <summary>
        /// Delete all word by userId
        /// </summary>
        public async Task<ResponseBase> DeleteAll(long userId)
        {
            try
            {
                var entities = await _unitOfWork.WordRepository.Query(x => x.UserId == userId).ToListAsync();

                var validationResult = _validator.IsValidWordList(entities);
                if (validationResult.HasError)
                {
                    return validationResult;
                }

                await _unitOfWork.WordRepository.DeleteManyAsync(entities);
                await _unitOfWork.SaveAsync();

                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return await _validator.CreateDeleteErrorResponse<ResponseBase>(ex.Message);
            }
        }

        /// <summary>
        /// Reset all word's CorrectCount and IncorrectCount property to 0
        /// </summary>
        public async Task<ListWordResponse> ResetResults(long userId)
        {
            try
            {
                var entities = await _unitOfWork.WordRepository.Query(x => x.UserId == userId).ToListAsync();

                var validationResult = _validator.IsValidWordList(entities);
                if (validationResult.HasError)
                {
                    return new ListWordResponse
                    {
                        ErrorMessage = validationResult.ErrorMessage
                    };
                }

                foreach (var entity in entities)
                {
                    entity.CorrectCount = 0;
                    entity.IncorrectCount = 0;
                }

                await _unitOfWork.WordRepository.UpdateManyAsync(entities);
                await _unitOfWork.SaveAsync();

                return await Task.FromResult(new ListWordResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = entities.Select(x => _factory.Create(x)).ToList()
                });

            }
            catch (Exception ex)
            {
                return await _validator.CreateUpdateErrorResponse<ListWordResponse>(ex.Message);
            }

        }

        #region export and import methods

        /// <summary>
        /// Export word list to txt file
        /// </summary>
        public async Task<ExportWordListToFileResponse> ExportWordListToTextFile(long userId)
        {
            try
            {
                var entities = await _unitOfWork.WordRepository.Query(x => x.UserId == userId).ToListAsync();
                StringBuilder builder = new StringBuilder();

                foreach (var entity in entities)
                {
                    builder.Append($"{entity.EnglishText},{entity.HungarianText};");
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(builder.ToString());
                MemoryStream stream = new MemoryStream(byteArray);


                return await Task.FromResult(new ExportWordListToFileResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = stream
                });
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ExportWordListToFileResponse>(ex.Message);
            }
        }

        /// <summary>
        /// Import word list from txt file
        /// </summary>
        public async Task<ResponseBase> ImportWordListFromTextFile(ImportWordListFromTextFileRequest request)
        {
            try
            {
                var words = request.Content.Split(';');

                foreach (var word in words)
                {
                    if (string.IsNullOrWhiteSpace(word)) continue;
                    var texts = word.Split(',');

                    if (texts.Length != 2)
                        return new ResponseBase
                        {
                            ErrorMessage = ErrorMessage.InvalidFileData
                        };


                    var entity = _factory.Create(new CreateWordFromImportedFileRequest
                    {
                        EnglishText = texts[0],
                        HungarianText = texts[1],
                        UserId = request.UserId
                    });

                    if (entity != null)
                    {
						//check exits words
						var validationResult = _validator.IsValidWordTextsExistsCheck(entity.EnglishText, entity.HungarianText, request.UserId);
                        if (!validationResult.HasError)
                        {
                            await _unitOfWork.WordRepository.CreateAsync(entity);
                        }
                    }
                }

                await _unitOfWork.SaveAsync();
                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ResponseBase>(ex.Message);
            }

        }

        /// <summary>
        /// Export word list to excel file
        /// </summary>
        public async Task<ExportWordListToFileResponse> ExportWordListToExcelFile(long userId)
        {
            try
            {
                var entities = await _unitOfWork.WordRepository.Query(x => x.UserId == userId).ToListAsync();

                // Create a new MemoryStream without using a using block
                var stream = new MemoryStream();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("WordList");

                    // Add headers
                    worksheet.Cell(1, 1).Value = "English Text";
                    worksheet.Cell(1, 2).Value = "Hungarian Text";

                    // Set header style
                    worksheet.Row(1).Style.Font.Bold = true;
                    worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Row(1).Style.Font.FontColor = XLColor.White;

                    // Add data
                    int row = 2;
                    foreach (var entity in entities)
                    {
                        worksheet.Cell(row, 1).Value = entity.EnglishText;
                        worksheet.Cell(row, 2).Value = entity.HungarianText;
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    // Convert the workbook to a MemoryStream
                    workbook.SaveAs(stream);
                }

                // Reset the MemoryStream's position to the beginning before returning it
                stream.Position = 0;

                return await Task.FromResult(new ExportWordListToFileResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = stream
                });

            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ExportWordListToFileResponse>(ex.Message);
            }
        }

		/// <summary>
		/// Import word list from Excel file
		/// </summary>
		public async Task<ResponseBase> ImportWordListFromExcelFile(ImportWordListFromExcelFileRequest request)
		{
			try
			{
				// Assuming you have only one worksheet in the Excel file
				var worksheet = request.Workbook.Worksheets.First();

				// Skip the header row
				var rows = worksheet.RowsUsed().Skip(1);

				foreach (var row in rows)
				{
					var englishText = row.Cell(1).Value.ToString();
					var hungarianText = row.Cell(2).Value.ToString();
			
					var entity = _factory.Create(new CreateWordFromImportedFileRequest
					{
						EnglishText = englishText,
						HungarianText = hungarianText,
						UserId = request.UserId
					});

					if (entity != null)
					{
						//check exits words
						var validationResult = _validator.IsValidWordTextsExistsCheck(entity.EnglishText, entity.HungarianText, request.UserId);
						if (!validationResult.HasError)
						{
							await _unitOfWork.WordRepository.CreateAsync(entity);
						}
					}
				}
				await _unitOfWork.SaveAsync();
				return new ResponseBase();
			}
			catch (Exception ex)
			{
				return await _validator.CreateServerErrorResponse<ResponseBase>(ex.Message);
			}
		}

		#endregion
	}
}
