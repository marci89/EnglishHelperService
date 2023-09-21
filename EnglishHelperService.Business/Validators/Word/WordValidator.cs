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

            if (_unitOfWork.WordRepository.Count(u => u.EnglishText == request.EnglishText && u.UserId == request.UserId) > 0)
                return CreateErrorResponse<CreateWordResponse>(ErrorMessage.EnglishWordExists);

            if (_unitOfWork.WordRepository.Count(u => u.HungarianText == request.HungarianText && u.UserId == request.UserId) > 0)
                return CreateErrorResponse<CreateWordResponse>(ErrorMessage.HungarianWordExists);

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
                if (_unitOfWork.WordRepository.Count(x => x.EnglishText == request.EnglishText && x.UserId == request.UserId) > 0)
                    return CreateErrorResponse<UpdateWordResponse>(ErrorMessage.EnglishWordExists);
            }

            //Check the current word, too because I want to able to change its different props. 
            if (currentWord != null && currentWord.HungarianText != request.HungarianText)
            {
                if (_unitOfWork.WordRepository.Count(u => u.HungarianText == request.HungarianText && u.UserId == request.UserId) > 0)
                    return CreateErrorResponse<UpdateWordResponse>(ErrorMessage.HungarianWordExists);
            }

            return new UpdateWordResponse
            {
                StatusCode = StatusCode.Created,
            };
        }
    }
}