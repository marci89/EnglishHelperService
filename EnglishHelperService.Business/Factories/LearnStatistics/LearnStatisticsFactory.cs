
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
        public Entity.LearnStatistics Create(CreateLearnStatisticsRequest request)
        {
            if (request is null)
                return null;

            return new Entity.LearnStatistics
            {
                UserId = request.UserId,
                CorrectCount = request.CorrectCount,
                IncorrectCount = request.IncorrectCount,
                Result = request.Result,
                LearnMode = Create(request.LearnMode),
                Created = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Map client LearnModeType from domain LearnModeType
        /// </summary>
        public LearnModeType Create(Entity.LearnModeType type)
        {
            switch (type)
            {
                case Entity.LearnModeType.Flashcard:
                    return LearnModeType.Flashcard;
                case Entity.LearnModeType.Typing:
                    return LearnModeType.Typing;
                case Entity.LearnModeType.Selection:
                    return LearnModeType.Selection;
                default:
                    throw new ArgumentException("Invalid LearnMode type.");
            }
        }

        /// <summary>
        /// Map domain LearnModeType from client LearnModeType
        /// </summary>
        public Entity.LearnModeType Create(LearnModeType type)
        {
            switch (type)
            {
                case LearnModeType.Flashcard:
                    return Entity.LearnModeType.Flashcard;
                case LearnModeType.Typing:
                    return Entity.LearnModeType.Typing;
                case LearnModeType.Selection:
                    return Entity.LearnModeType.Selection;
                default:
                    throw new ArgumentException("Invalid LearnMode type.");
            }
        }
    }
}


