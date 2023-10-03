using EnglishHelperService.Persistence.Repositories;
using EnglishHelperService.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.Business
{

    /// <summary>
    /// Managing learn statistics
    /// </summary>
    public class LearnStatisticsService : ILearnStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LearnStatisticsFactory _factory;
        private readonly LearnStatisticsValidator _validator;

        public LearnStatisticsService(
            IUnitOfWork unitOfWork,
            LearnStatisticsFactory factory,
            LearnStatisticsValidator validator
            )
        {
            _unitOfWork = unitOfWork;
            _factory = factory;
            _validator = validator;
        }



        /// <summary>
        /// List learn statistics by user id
        /// </summary>
        public async Task<ListLearnStatisticsResponse> List(long userId)
        {
            try
            {
                var entities = await _unitOfWork.LearnStatisticsRepository.Query(x => x.UserId == userId).OrderBy(x => x.Created).ToListAsync();

                return await Task.FromResult(new ListLearnStatisticsResponse
                {
                    StatusCode = StatusCode.Ok,
                    Result = entities.Select(x => _factory.Create(x)).ToList()
                });
            }
            catch (Exception ex)
            {
                return await _validator.CreateServerErrorResponse<ListLearnStatisticsResponse>(ex.Message);
            }
        }

        /// <summary>
        /// Create learn statistics by logined user id
        /// </summary>
        public async Task<CreateLearnStatisticsResponse> Create(CreateLearnStatisticsRequest request, long userId)
        {
            try
            {
                var validationResult = _validator.IsValidCreateRequest(request);
                if (!validationResult.HasError)
                {
                    var entity = _factory.Create(request, userId);
                    await _unitOfWork.LearnStatisticsRepository.CreateAsync(entity);
                    await _unitOfWork.SaveAsync();

                    return new CreateLearnStatisticsResponse
                    {
                        StatusCode = StatusCode.Created,
                        Result = _factory.Create(entity)
                    };
                }
                return validationResult;
            }
            catch (Exception ex)
            {
                return await _validator.CreateCreationErrorResponse<CreateLearnStatisticsResponse>(ex.Message);
            }
        }


        /// <summary>
        /// Delete learn statistics by id
        /// </summary>
        public async Task<ResponseBase> Delete(long id)
        {
            try
            {
                var entity = await _unitOfWork.LearnStatisticsRepository.ReadAsync(u => u.Id == id);
                if (entity is null)
                {
                    return await _validator.CreateNotFoundResponse<ResponseBase>();
                }

                await _unitOfWork.LearnStatisticsRepository.DeleteAsync(entity);
                await _unitOfWork.SaveAsync();

                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return await _validator.CreateDeleteErrorResponse<ResponseBase>(ex.Message);
            }
        }

        /// <summary>
        /// Delete all learn statistics by userId
        /// </summary>
        public async Task<ResponseBase> DeleteAll(long userId)
        {
            try
            {
                var entities = await _unitOfWork.LearnStatisticsRepository.Query(x => x.UserId == userId).ToListAsync();
                var validationResult = _validator.IsValidLearnStatisticsList(entities);
                if (validationResult.HasError)
                {
                    return validationResult;
                }
                await _unitOfWork.LearnStatisticsRepository.DeleteManyAsync(entities);
                await _unitOfWork.SaveAsync();

                return new ResponseBase();
            }
            catch (Exception ex)
            {
                return await _validator.CreateDeleteErrorResponse<ResponseBase>(ex.Message);
            }
        }
    }
}
