using EnglishHelperService.Persistence.Common;
using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Persistence.Repositories
{
    /// <summary>
    ///  LearnStatistics repository with generic
    /// </summary>
    public class LearnStatisticsRepository : GenericRepository<DataContext, LearnStatistics>, ILearnStatisticsRepository
    {
        public LearnStatisticsRepository(DataContext dbContext) : base(dbContext) { }
    }
}
