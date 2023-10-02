

namespace EnglishHelperService.ServiceContracts
{
    /// <summary>
    /// List user filtering request with paging
    /// </summary>
    public class ListUserWithFilterRequest : PagedListRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
    }
}
