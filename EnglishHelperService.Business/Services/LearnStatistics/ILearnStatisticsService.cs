using EnglishHelperService.ServiceContracts;

namespace EnglishHelperService.Business
{
    public interface ILearnStatisticsService
    {
        Task<ListLearnStatisticsResponse> List(long userId);
        Task<CreateLearnStatisticsResponse> Create(CreateLearnStatisticsRequest request);
        Task<ResponseBase> Delete(long id);
        Task<ResponseBase> DeleteAll(long userId);
    }
}
