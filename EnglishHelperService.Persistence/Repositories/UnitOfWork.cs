using EnglishHelperService.Persistence.Common;

namespace EnglishHelperService.Persistence.Repositories
{
    /// <summary>
    /// class that combine database operations(repositories).
    /// </summary>
    public class UnitOfWork : UnitOfWorkBase<DataContext>, IUnitOfWork
    {
        public UnitOfWork(
            DataContext context,
            IUserRepository userRepository,
            IWordRepository wordRepository,
            ILearnStatisticsRepository learnStatisticsRepository
            ) : base(context)
        {
            UserRepository = userRepository;
            WordRepository = wordRepository;
            LearnStatisticsRepository = learnStatisticsRepository;
        }

        public virtual IUserRepository UserRepository { get; }
        public virtual IWordRepository WordRepository { get; }
        public virtual ILearnStatisticsRepository LearnStatisticsRepository { get; }

    }
}
