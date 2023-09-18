using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;

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

            return new UpdateWordResponse
            {
                StatusCode = StatusCode.Created,
            };
        }
    }
}