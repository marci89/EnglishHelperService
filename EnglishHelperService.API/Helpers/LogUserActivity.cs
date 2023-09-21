using EnglishHelperService.API.Extensions;
using EnglishHelperService.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace EnglishHelperService.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogUserActivity(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var user = await _unitOfWork.UserRepository.Query(u => u.Id == userId).FirstOrDefaultAsync();
            user.LastActive = DateTime.UtcNow;
            await _unitOfWork.UserRepository.UpdateAsync(user);
        }
    }
}
