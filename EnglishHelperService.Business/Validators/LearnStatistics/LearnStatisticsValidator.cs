using EnglishHelperService.ServiceContracts;
using Entity = EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{
    /// <summary>
    /// Validating LearnStatistics object
    /// </summary>
    public class LearnStatisticsValidator : BaseValidator
    {
        /// <summary>
        /// Execute LearnStatistics create request validating
        /// </summary>
        public CreateLearnStatisticsResponse IsValidCreateRequest(CreateLearnStatisticsRequest request)
        {
            if (request is null)
                return CreateErrorResponse<CreateLearnStatisticsResponse>(ErrorMessage.InvalidRequest);

            return new CreateLearnStatisticsResponse
            {
                StatusCode = StatusCode.Created,
            };
        }

        /// <summary>
        /// Execute LearnStatistics list null check validating
        /// </summary>
        public ResponseBase IsValidLearnStatisticsList(List<Entity.LearnStatistics> request)
        {
            if (request is null || !request.Any())
                return CreateErrorResponse<ResponseBase>(ErrorMessage.NoElementToModify);

            return new ResponseBase
            {
                StatusCode = StatusCode.Ok,
            };
        }
    }
}
