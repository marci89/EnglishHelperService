
using EnglishHelperService.ServiceContracts;
using Entity = EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Business
{

    /// <summary>
    /// Word object mapping
    /// </summary>
    public class WordFactory
    {
        private readonly WordValidator _validator;

        public WordFactory(WordValidator validator)
        {
            _validator = validator;
        }

        /// <summary>
        /// Map client word from domain word
        /// </summary>
        public Word Create(Entity.Word request)
        {
            if (request is null)
                return null;

            return new Word
            {
                Id = request.Id,
                UserId = request.UserId,
                EnglishText = request.EnglishText,
                HungarianText = request.HungarianText,
                CorrectCount = request.CorrectCount,
                IncorrectCount = request.IncorrectCount,
                Created = request.Created,
                LastUse = request.LastUse,
            };
        }

        /// <summary>
        /// Map domain word from client word
        /// </summary>
        public Entity.Word Create(CreateWordRequest request)
        {
            if (request is null)
                return null;

            return new Entity.Word
            {
                UserId = request.UserId,
                EnglishText = request.EnglishText,
                HungarianText = request.HungarianText,
                CorrectCount = 0,
                IncorrectCount = 0,
                Created = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Map domain word from imported file
        /// </summary>
        public Entity.Word Create(CreateWordFromImportedFileRequest request)
        {
            if (request is null
                || string.IsNullOrWhiteSpace(request.EnglishText)
                || string.IsNullOrWhiteSpace(request.HungarianText))
                return null;

            return new Entity.Word
            {
                UserId = request.UserId,
                EnglishText = request.EnglishText,
                HungarianText = request.HungarianText,
                CorrectCount = 0,
                IncorrectCount = 0,
                Created = DateTime.UtcNow
            };
        }
    }
}
