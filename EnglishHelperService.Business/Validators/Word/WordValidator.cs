using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;
using Entity = EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{

    /// <summary>
    /// Validating word object
    /// </summary>
    public class WordValidator : BaseValidator
    {
        private readonly IUnitOfWork _unitOfWork;

        public WordValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Execute word create request validating
        /// </summary>
        public CreateWordResponse IsValidCreateRequest(CreateWordRequest request)
        {
            if (request is null)
                return CreateErrorResponse<CreateWordResponse>(ErrorMessage.InvalidRequest);

            if (String.IsNullOrWhiteSpace(request.EnglishText))
                return CreateErrorResponse<CreateWordResponse>(ErrorMessage.EnglishTextRequired);

            if (String.IsNullOrWhiteSpace(request.HungarianText))
                return CreateErrorResponse<CreateWordResponse>(ErrorMessage.HungarianTextRequired);

            var englishTextExistsValidation = IsValidEnglishTextExistsCheck(request.EnglishText, request.UserId);
            if (englishTextExistsValidation.HasError && englishTextExistsValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<CreateWordResponse>(englishTextExistsValidation.ErrorMessage.Value);
            }

            var hungarianTextExistsValidation = IsValidHungarianTextExistsCheck(request.HungarianText, request.UserId);
            if (hungarianTextExistsValidation.HasError && hungarianTextExistsValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<CreateWordResponse>(hungarianTextExistsValidation.ErrorMessage.Value);
            }

            return new CreateWordResponse
            {
                StatusCode = StatusCode.Created,
            };
        }

        /// <summary>
        /// Execute word update request validating
        /// </summary>
        public UpdateWordResponse IsValidUpdateRequest(UpdateWordRequest request)
        {
            if (request is null)
                return CreateErrorResponse<UpdateWordResponse>(ErrorMessage.InvalidRequest);

            if (String.IsNullOrWhiteSpace(request.EnglishText))
                return CreateErrorResponse<UpdateWordResponse>(ErrorMessage.EnglishTextRequired);

            if (String.IsNullOrWhiteSpace(request.HungarianText))
                return CreateErrorResponse<UpdateWordResponse>(ErrorMessage.HungarianTextRequired);


            var currentWord = _unitOfWork.WordRepository.Query(x => x.Id == request.Id).FirstOrDefault();

            //Check the current word, too because I want to able to change it's different props. 
            if (currentWord != null && currentWord.EnglishText != request.EnglishText)
            {
                var englishTextExistsValidation = IsValidEnglishTextExistsCheck(request.EnglishText, request.UserId);
                if (englishTextExistsValidation.HasError && englishTextExistsValidation.ErrorMessage.HasValue)
                {
                    return CreateErrorResponse<UpdateWordResponse>(englishTextExistsValidation.ErrorMessage.Value);
                }
            }

            //Check the current word, too because I want to able to change its different props. 
            if (currentWord != null && currentWord.HungarianText != request.HungarianText)
            {
                var hungarianTextExistsValidation = IsValidHungarianTextExistsCheck(request.HungarianText, request.UserId);
                if (hungarianTextExistsValidation.HasError && hungarianTextExistsValidation.ErrorMessage.HasValue)
                {
                    return CreateErrorResponse<UpdateWordResponse>(hungarianTextExistsValidation.ErrorMessage.Value);
                }
            }

            return new UpdateWordResponse
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute word list null check validating
        /// </summary>
        public ResponseBase IsValidWordList(List<Entity.Word> request)
        {
            if (request is null || !request.Any())
                return CreateErrorResponse<ResponseBase>(ErrorMessage.NoElementToModify);

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }

        /// <summary>
        /// Execute Word's texts exists checking
        /// </summary>
        public ResponseBase IsValidWordTextsExistsCheck(string englishText, string hungarianText, long userId)
        {
            var englishTextExistsValidation = IsValidEnglishTextExistsCheck(englishText, userId);
            if (englishTextExistsValidation.HasError && englishTextExistsValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(englishTextExistsValidation.ErrorMessage.Value);
            }

            var hungarianTextExistsValidation = IsValidHungarianTextExistsCheck(hungarianText, userId);
            if (hungarianTextExistsValidation.HasError && hungarianTextExistsValidation.ErrorMessage.HasValue)
            {
                return CreateErrorResponse<ResponseBase>(hungarianTextExistsValidation.ErrorMessage.Value);
            }

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }



        #region Private methods

        /// <summary>
        /// Execute english text exists check validating
        /// </summary>
        private ResponseBase IsValidEnglishTextExistsCheck(string englishText, long userId)
        {
            if (_unitOfWork.WordRepository.Count(u => u.EnglishText == englishText && u.UserId == userId) > 0)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.EnglishWordExists);

            return new ResponseBase { StatusCode = StatusCode.Ok };
        }

        /// <summary>
        /// Execute hungarian text exists check validating
        /// </summary>
        private ResponseBase IsValidHungarianTextExistsCheck(string hungarianText, long userId)
        {
            if (_unitOfWork.WordRepository.Count(u => u.HungarianText == hungarianText && u.UserId == userId) > 0)
                return CreateErrorResponse<ResponseBase>(ErrorMessage.HungarianWordExists);

            return new ResponseBase { StatusCode = StatusCode.Ok };
        }

        #endregion
    }
}