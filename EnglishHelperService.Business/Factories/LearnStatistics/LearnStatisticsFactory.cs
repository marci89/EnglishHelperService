
using EnglishHelperService.ServiceContracts;
using Entity = EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{
    /// <summary>
    /// LearnStatistics object mapping
    /// </summary>
    public class LearnStatisticsFactory
    {
        /// <summary>
        /// Map client LearnStatistics from domain LearnStatistics
        /// </summary>
        public LearnStatistics Create(Entity.LearnStatistics request)
        {
            if (request is null)
                return null;

            return new LearnStatistics
            {
                Id = request.Id,
                UserId = request.UserId,
                CorrectCount = request.CorrectCount,
                IncorrectCount = request.IncorrectCount,
                Result = request.Result,
                LearnMode = Create(request.LearnMode),
                Created = request.Created,
            };
        }

        /// <summary>
        /// Map domain LearnStatistics from client LearnStatistics
        /// </summary>
        public Entity.LearnStatistics Create(CreateLearnStatisticsRequest request, long userId)
        {
            if (request is null)
                return null;

            return new Entity.LearnStatistics
            {
                UserId = userId,
                CorrectCount = request.CorrectCount,
                IncorrectCount = request.IncorrectCount,
                Result = request.Result,
                LearnMode = Create(request.LearnMode),
                Created = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Create a LearnStatisticsChartResult object
        /// </summary>
        public LearnStatisticsChartResult Create(List<Entity.LearnStatistics> entites, int quantity)
        {
            if (entites is null)
                return null;

            List<string> label = Enumerable.Range(1, quantity).Select(i => i.ToString()).ToList();

            return new LearnStatisticsChartResult
            {
                ChartLabel = label,
                FlashcardChartData = Create(entites, quantity, Entity.LearnModeType.Flashcard),
                TypingChartData = Create(entites, quantity, Entity.LearnModeType.Typing),
                SelectionChartData = Create(entites, quantity, Entity.LearnModeType.Selection),
                ListeningChartData = Create(entites, quantity, Entity.LearnModeType.Listening)
            };
        }


        #region private methods

        /// <summary>
        /// Create a ChartData
        /// </summary>
        private List<string> Create(List<Entity.LearnStatistics> entites, int quantity, Entity.LearnModeType type)
        {
            var data = entites.Where(x => x.LearnMode == type).OrderByDescending(x => x.Created).Take(quantity).Select(x => x.Result.ToString()).ToList();
            data.Reverse();
            return data;
        }

        /// <summary>
        /// Map client LearnModeType from domain LearnModeType
        /// </summary>
        private LearnModeType Create(Entity.LearnModeType type)
        {
            switch (type)
            {
                case Entity.LearnModeType.Flashcard:
                    return LearnModeType.Flashcard;
                case Entity.LearnModeType.Typing:
                    return LearnModeType.Typing;
                case Entity.LearnModeType.Selection:
                    return LearnModeType.Selection;
                case Entity.LearnModeType.Listening:
                    return LearnModeType.Listening;
                default:
                    throw new ArgumentException("Invalid LearnMode type.");
            }
        }

        /// <summary>
        /// Map domain LearnModeType from client LearnModeType
        /// </summary>
        private Entity.LearnModeType Create(LearnModeType type)
        {
            switch (type)
            {
                case LearnModeType.Flashcard:
                    return Entity.LearnModeType.Flashcard;
                case LearnModeType.Typing:
                    return Entity.LearnModeType.Typing;
                case LearnModeType.Selection:
                    return Entity.LearnModeType.Selection;
                case LearnModeType.Listening:
                    return Entity.LearnModeType.Listening;
                default:
                    throw new ArgumentException("Invalid LearnMode type.");
            }
        }

        #endregion
    }
}


