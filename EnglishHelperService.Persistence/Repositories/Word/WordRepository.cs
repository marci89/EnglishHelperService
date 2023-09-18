using EnglishHelperService.Persistence.Common;
using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Persistence.Repositories
{

    /// <summary>
    /// Word repository with generic
    /// </summary>
    public class WordRepository : GenericRepository<DataContext, Word>, IWordRepository
    {
        public WordRepository(DataContext dbContext) : base(dbContext) { }
    }
}