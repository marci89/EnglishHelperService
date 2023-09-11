namespace EnglishHelperService.ServiceContracts
{
	/// <summary>
	/// Response base class that helps send the correct objectto the client
	/// and send correct error messages if that has any
	/// </summary>
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

