namespace EnglishHelperService.ServiceContracts
{
	public class ResponseBase
	{
		/// <summary>
		/// Status code
		/// </summary>
		public virtual StatusCode StatusCode { get; set; }

		private ErrorMessage? _errorMessage;

		/// <summary>
		/// Returning error message
		/// </summary>
		public virtual ErrorMessage? ErrorMessage
		{
			get
			{
				return _errorMessage;
			}
			set
			{
				_errorMessage = value;
			}
		}

		/// <summary>
		/// Has error check
		/// </summary>
		public virtual bool HasError => ErrorMessage.HasValue;
	}
}

